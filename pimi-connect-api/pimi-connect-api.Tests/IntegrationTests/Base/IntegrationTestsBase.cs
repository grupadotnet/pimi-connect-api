using AutoMapper;
using Microsoft.Extensions.Configuration;
using pimi_connect_api.Tests.Shared;
using pimi_connect_api.UnitTests.Shared;
using pimi_connect_app.Data.AppDbContext;

namespace pimi_connect_api.Tests.IntegrationTests.Base;

public abstract class IntegrationTestsBase<TDtoType>  where TDtoType : class
{
    #region Properties
    protected AppDbContext TestDbContext { get; private set; }
    protected IConfiguration Configuration { get; private set; }
    protected IntegrationTestsSettings Settings { get; private set; }
    protected IMapper Mapper { get; private set; }
    protected TestHelper Helper { get; }
    protected List<Guid> ExistingIds { get; private set; }
    protected Guid ExistingId { get; private set; }
    protected Guid NotExistingId { get; private set; }
    #endregion
    
    protected IntegrationTestsBase()
    {
        SetupConfiguration();
        SetupSettings();
        SetupMapper();
        SetupTestDbContext();
        
        SetIds();
        
        Helper = new TestHelper(TestDbContext, Mapper);
    }
    
    protected async Task Arrange(bool fillDb = true)
    {
        // reset DbContext
        await Helper.ResetDbContext();
    }
    
    #region Get status and content from result
    protected (int Status, TDtoType? Content) GetStatusAndContentFromResult(IActionResult? result)
    {
        return GetStatusAndContentFromResult<TDtoType>(result);
    }
        
    protected (int Status, List<TDtoType>? List) GetStatusAndListFromResult(IActionResult? result)
    {
        return GetStatusAndContentFromResult<List<TDtoType>>(result);
    }
    
    protected (int Status, T? Content) GetStatusAndContentFromResult<T>
        (IActionResult? result) where T: class
    {
        try
        {
            var resultStatusCode = (int)result
                .GetType()
                .GetProperty("StatusCode")!
                .GetValue(result, null)!;

            var resultContent = (T)result
                .GetType()
                .GetProperty("Value")!
                .GetValue(result, null)!;
            
            return (resultStatusCode, resultContent);
        }
        catch (Exception e)
        {
            var statusCodeResult = result as StatusCodeResult;

            if (statusCodeResult is null)
                return (-1, null);
                
            return (statusCodeResult.StatusCode, null);
        }
    }
    #endregion

    #region Setup Configuration, Mapper and DbContext
    private void SetupConfiguration()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Tests.json")
            .Build();

        Configuration = configuration;
    }

    private void SetupSettings()
    {
        Settings = new IntegrationTestsSettings();
        Configuration.GetSection("IntegrationTestsSettings")
            .Bind(Settings, c => c.BindNonPublicProperties = true);

        if (Settings == null)
        {
            throw new InvalidOperationException("Could not get IntegrationTestsSettings correctly.");
        }
    }
    
    private void SetupMapper()
    {
        Mapper = Utils.CreateMapper();
    }
    
    private void SetupTestDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString(Settings.ConnectionStringName), 
            b => b.MigrationsAssembly(Settings.MigrationsAssemblyName));

        TestDbContext = new AppDbContext(optionsBuilder.Options);
    }
    
    #endregion

    #region Set ExistingIds, ExistingId and NotExistingId
    private void SetIds()
    {
        SetExistingIds();
        SetExistingId();
        SetNotExistingId();
    }
    
    private void SetExistingIds()
    {
        ExistingIds = Utils.CreateGuids(Settings.EntitiesCount);
    }

    private void SetExistingId()
    {
        var r = new Random();
        
        ExistingId = ExistingIds[r.Next(0, ExistingIds.Count)];
    }

    private void SetNotExistingId()
    {
        var newGuid = Guid.NewGuid();

        while (ExistingIds.Contains(newGuid))
        {
            newGuid = Guid.NewGuid();
        }

        NotExistingId = newGuid;
    }
    #endregion
}