using MongoDB.Driver;
using MongoDB.Net6.Model.Dtos;
using MongoDB.Net6.Model.Entities;

namespace MongoDB.Net6.Core
{
    public class CrudMongoDB : ICrudMongoDB
    {
        private readonly IMongoClient _mongoClient;
        public const string Database = "school";
        public const string Collection = "people";

        public CrudMongoDB(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public Task<List<People>> List()
        {
            var database = _mongoClient.GetDatabase(Database);
            var peopleDB = database.GetCollection<People>(Collection);

            Task<List<People>> listPeople = peopleDB.Find(d => true).ToListAsync();

            return listPeople;
        }

        public Task<bool> Insert(PeopleDto peopleDto)
        {
            try
            {
                var database = _mongoClient.GetDatabase(Database);
                var peopleDB = database.GetCollection<People>(Collection);

                var people = new People()
                {
                    Name = peopleDto.Name,
                    Age = peopleDto.Age
                };

                peopleDB.InsertOne(people);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }


        public Task<bool> Update(PeopleDto peopleDto)
        {
            try
            {
                if (string.IsNullOrEmpty(peopleDto.Id))
                {
                    return Task.FromResult(false);
                }

                var database = _mongoClient.GetDatabase(Database);
                var peopleDB = database.GetCollection<People>(Collection);

                var people = new People()
                {
                    Id = peopleDto.Id!,
                    Name = peopleDto.Name,
                    Age = peopleDto.Age
                };

                peopleDB.ReplaceOne(u => u.Id == people.Id, people);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return Task.FromResult(false);
                }

                var database = _mongoClient.GetDatabase(Database);
                var peopleDB = database.GetCollection<People>(Collection);

                peopleDB.DeleteOne(d => d.Id == id);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
    }
}
