using AutoMapper;
using pimi_connect_app.Data.MappingProfiles;

namespace pimi_connect_api.Tests.Shared;

public static class Utils
{
    private static Random _random = new Random();
    
    public static IMapper CreateMapper()
    {
        var mapperConfig = new MapperConfiguration(cfg => 
        {
            cfg.AddProfile(new MappingProfile());
        });

        return new Mapper(mapperConfig);
    }

    public static List<Guid> CreateGuids(int count)
    {
        var result = new List<Guid>();
        
        for (var i = 0; i < count; i++)
        {
            result.Add(Guid.NewGuid());
        }

        return result;
    }

    public static List<string> CreateDummyStrings(int count, int maxLength)
    {
        var result = new List<string>();

        for (var i = 0; i < count; i++)
        {
            result.Add(RandomString(_random.Next(maxLength)));
        }
        
        return result;
    }
    
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}