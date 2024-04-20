using AutoMapper;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Enums;

namespace pimi_connect_api.UnitTests.Shared;

public class TestHelper
{
    private readonly AppDbContext _testDbContext;
    private readonly IMapper _mapper;
    
    public TestHelper(AppDbContext testDbContext, IMapper mapper)
    {
        _testDbContext = testDbContext;
        _mapper = mapper;
    }

    public async Task ResetDbContext()
    {
        await _testDbContext.Database.EnsureDeletedAsync();
        await _testDbContext.Database.EnsureCreatedAsync();
    }

    #region Fill tables
    public async Task FillUsersTable(List<Guid> idsToAdd, List<string> domainsToAdd)
    {
        for(int i = 0; i < idsToAdd.Count; i++)
        {
            var newUser = GenerateUserEntity(domainsToAdd[i], idsToAdd[i]);

            await _testDbContext
                .Users.AddAsync(newUser);
        }

        ;
        await _testDbContext.SaveChangesAsync();
        _testDbContext.ChangeTracker.Clear();
    }
    
    public async Task FillAttachmentsTable(List<Guid> idsToAdd)
    {
        foreach (var id in idsToAdd)
        {
            var newAttachment = GenerateAttachmentEntity(id);

            await _testDbContext
                .Attachments.AddAsync(newAttachment);
        }
        
        await _testDbContext.SaveChangesAsync();
        _testDbContext.ChangeTracker.Clear();
    }
    #endregion
    
}