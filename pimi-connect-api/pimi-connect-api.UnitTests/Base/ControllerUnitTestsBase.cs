using AutoMapper;
using Microsoft.Extensions.Configuration;
using pimi_connect_api.UnitTests.Shared;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.MappingProfiles;

namespace pimi_connect_api.UnitTests.Base;

public abstract class ControllerUnitTestsBase<TDtoType>: TestSettings  where TDtoType : class
{
    #region Properties
    protected IConfiguration Configuration { get; private set; }
    protected IMapper Mapper { get; private set; }
    protected AppDbContext TestDbContext { get; private set; }
    protected TestHelper Helper { get; }
    protected int ExistingIndex { get; private set; }
    protected int NotExistingIndex { get; private set; }
    #endregion
    
    protected ControllerUnitTestsBase()
    {
        SetupConfiguration();
        SetupMapper();
        SetupTestDbContext();
        SetExistingIndex();
        SetNotExistingIndex();
        Helper = new TestHelper(TestDbContext);
    }

    protected abstract TDtoType CreateDto(int i);

    protected async Task Arrange(bool fillDb = true)
    {
        // reset DbContext
        await Helper.ResetDbContext();
    }
    
    #region GetStatusAndContentFromResult
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
            .AddJsonFile(ConfigurationFileName)
            .Build();

        Configuration = configuration;
    }
    
    private void SetupMapper()
    {
        var mapperConfig = new MapperConfiguration(cfg => 
        {
            cfg.AddProfile(new MappingProfile()); 
        });

        Mapper = new Mapper(mapperConfig);
    }
    
    private void SetupTestDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString(ConnectionStringName), 
            b => b.MigrationsAssembly(MigrationsAssemblyName));

        TestDbContext = new AppDbContext(optionsBuilder.Options);
    }
    
    #endregion

    #region Set ExistingIndex and NotExistingIndex

    private void SetExistingIndex()
    {
        var r = new Random();
        ExistingIndex = r.Next(0, EntitiesCount);
    }

    private void SetNotExistingIndex()
    {
        NotExistingIndex = EntitiesCount + 1;
    }

    #endregion
}