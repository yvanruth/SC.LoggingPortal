namespace SC.LoggingPortal.Data.Persistence
{
    using MongoDB.Driver;
    using SC.LoggingPortal.Data.Entity;
    using SC.LoggingPortal.Mongo;
    using SC.LoggingPortal.Mongo.Attributes;

    public class LoggingDbContext : MongoDbContext
    {
        public LoggingDbContext(string connectionStringOrName)
            : base(connectionStringOrName)
        {
        }

        [Collection(Name="Log")]
        public IMongoCollection<LogMessage> LogMessages { get; set; }
    }
}
