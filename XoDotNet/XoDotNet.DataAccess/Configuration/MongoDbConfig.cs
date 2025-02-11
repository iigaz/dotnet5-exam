namespace XoDotNet.DataAccess.Configuration;

public class MongoDbConfig
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string GameStateCollectionName { get; set; } = null!;
    public string UserRatingCollectionName { get; set; } = null!;
}