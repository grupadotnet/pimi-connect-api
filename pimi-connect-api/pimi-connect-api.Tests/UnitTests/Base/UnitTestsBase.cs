using AutoMapper;
using pimi_connect_api.Tests.Shared;
using pimi_connect_app.Data.AppDbContext;
using Xunit.Abstractions;

namespace pimi_connect_api.Tests.UnitTests.Base;

public abstract class UnitTestsBase
{
    protected readonly ITestOutputHelper Output;
    protected readonly IMapper Mapper;
    protected readonly List<Guid> DummyIds;
    protected readonly Guid DummyId;
    protected readonly List<string> DummyStrings;
    protected readonly string DummyString;
    
    protected UnitTestsBase(ITestOutputHelper output)
    {
        Output = output;
        Mapper = Utils.CreateMapper();
        
        DummyIds = Utils.CreateGuids(UnitTestsSettings.DummyCount);
        DummyId = DummyIds[0];
        DummyStrings = Utils.CreateDummyStrings(UnitTestsSettings.DummyCount, UnitTestsSettings.DummyStringMaxLength);
        DummyString = DummyStrings[0];
    }
}