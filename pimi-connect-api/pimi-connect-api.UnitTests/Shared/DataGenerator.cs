namespace pimi_connect_api.UnitTests.Shared;

public static class DataGenerator
{
    public static Guid GenerateGuid()
    {
        return new Guid();
    }

    public static string GenerateUserName(int i = -1)
    {
        return $"user{i}";
    }
    
    public static string GenerateEmail(int i = -1)
    {
        return $"user{i}@domain.com";
    }
    
}