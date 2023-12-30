using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Enums;

namespace pimi_connect_api.UnitTests.Shared;

public class TestHelper
{
    private readonly AppDbContext _testDbContext;
    
    public TestHelper(AppDbContext testDbContext)
    {
        _testDbContext = testDbContext;
    }

    public async Task ResetDbContext()
    {
        await _testDbContext.Database.EnsureDeletedAsync();
        await _testDbContext.Database.EnsureCreatedAsync();
    }

    public void TruncateTable(string tableName)
    {
        _testDbContext.Database
            .ExecuteSqlRaw($"TRUNCATE \"{tableName}\" CASCADE;");
    }

    #region Fill table methods

    public async Task FillUsersTable(int entityCount)
    {
        for (var i = 0; i < entityCount; i++)
        {
            var newUser = new UserEntity()
            {
                UserName = GenerateUserName(i),
                Email = GenerateEmail(i),
                Status = UserStatus.Accessible,
                ProfilePictureKey = GenerateGuid()
            };

            await _testDbContext
                .Users.AddAsync(newUser);
        }

        await _testDbContext.SaveChangesAsync();
        _testDbContext.ChangeTracker.Clear();
    }

    #endregion
    
}