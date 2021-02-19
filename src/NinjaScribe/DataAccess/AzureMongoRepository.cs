using MongoDB.Driver;
using System.Threading.Tasks;

namespace NinjaScribe.DataAccess
{
    public interface IAzureMongoRepository
    {
        Task InsertAsync(string collectionName, Visit visit);
    }

    public class AzureMongoRepository : IAzureMongoRepository
    {
        public AzureMongoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task InsertAsync(string collectionName, Visit visit)
        {
            var client = new MongoClient(_connectionString);
            var database = client.GetDatabase("NinjaTracker");
            var collection = database.GetCollection<Visit>(collectionName);
            await collection.InsertOneAsync(visit);
        }

        private readonly string _connectionString;
    }
}
