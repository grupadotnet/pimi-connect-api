using AutoMapper;
using Microsoft.Extensions.Configuration;
using pimi_connect_api.Tests.Shared;
using pimi_connect_app.Data.AppDbContext;
using Xunit.Abstractions;

namespace pimi_connect_api.Tests.UnitTests.Base;

public abstract class UnitTestsBase
{
    protected readonly ITestOutputHelper Output;
    protected readonly UnitTestsSettings Settings;
    protected readonly IMapper Mapper;
    protected readonly List<Guid> DummyIds;
    protected readonly Guid DummyId;
    protected readonly List<string> DummyStrings;
    protected readonly string DummyString;

    private IConfiguration _configuration;
    
    protected UnitTestsBase(ITestOutputHelper output)
    {
        Output = output;
        
        _configuration = SetupConfiguration();
        Settings = SetupSettings();
        
        
        Mapper = Utils.CreateMapper();
        
        DummyIds = Utils.CreateGuids(Settings.DummyCount);
        DummyId = DummyIds[0];
        DummyStrings = Utils.CreateDummyStrings(Settings.DummyCount, Settings.DummyStringMaxLength);
        DummyString = DummyStrings[0];
    }
    
    private IConfiguration SetupConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.Tests.json")
            .Build();
    }
    
    private UnitTestsSettings SetupSettings()
    {
        var settings = new UnitTestsSettings();
        _configuration.GetSection("UnitTestsSettings")
            .Bind(settings, c => c.BindNonPublicProperties = true);
        
        Output.WriteLine(settings.ToString());
        
        if (settings == null)
        {
            throw new InvalidOperationException("Could not get UnitTestsSettings correctly.");
        }

        return settings;
    }
}