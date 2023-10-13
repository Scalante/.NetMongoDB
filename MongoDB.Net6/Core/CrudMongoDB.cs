using MongoDB.Driver;
using MongoDB.Net6.Model.Entities;

namespace MongoDB.Net6.Core
{
    public class CrudMongoDB<TDocument> : ICrudMongoDB<TDocument> where TDocument : BaseDocument
    {
        private readonly IMongoClient _mongoClient;
        public const string Database = "school";
        public const string Collection = "student";

        public CrudMongoDB(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public async Task<IQueryable<TDocument>> ListAsync()
        {
            try
            {
                var database = _mongoClient.GetDatabase(Database);
                var collection = database.GetCollection<TDocument>(Collection);
                var queryable = collection.AsQueryable();

                return await Task.FromResult(queryable);
            }
            catch (Exception)
            {
                return Enumerable.Empty<TDocument>().AsQueryable();
            }
        }

        public async Task<bool> InsertAsync(TDocument model)
        {
            try
            {
                var database = _mongoClient.GetDatabase(Database);
                var collection = database.GetCollection<TDocument>(Collection);
                await collection.InsertOneAsync(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TDocument model)
        {
            try
            {
                var database = _mongoClient.GetDatabase(Database);
                var collection = database.GetCollection<TDocument>(Collection);

                var filter = Builders<TDocument>.Filter.Eq(x => x.Id, model.Id);
                await collection.ReplaceOneAsync(filter, model);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var database = _mongoClient.GetDatabase(Database);
                var collection = database.GetCollection<TDocument>(Collection);

                var filter = Builders<TDocument>.Filter.Eq(x => x.Id, id);
                await collection.DeleteOneAsync(filter);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
