using MongoDB.Driver;
using MongoDB.Net6.Model.Dtos;
using MongoDB.Net6.Model.Entities;

namespace MongoDB.Net6.Core
{
    public class CrudMongoDB : ICrudMongoDB
    {
        private readonly IMongoClient _mongoClient;
        public const string Database = "school";
        public const string Collection = "student";

        public CrudMongoDB(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public Task<List<Student>> List()
        {
            var database = _mongoClient.GetDatabase(Database);
            var studentCollection = database.GetCollection<Student>(Collection);

            Task<List<Student>> listPeople = studentCollection.Find(d => true).ToListAsync();

            return listPeople;
        }

        public Task<bool> Insert(StudentDto peopleDto)
        {
            try
            {
                var database = _mongoClient.GetDatabase(Database);
                var studentCollection = database.GetCollection<Student>(Collection);

                var people = new Student()
                {
                    Name = peopleDto.Name,
                    Age = peopleDto.Age,
                    BirthDate = peopleDto.BirthDate
                };

                studentCollection.InsertOneAsync(people);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }


        public Task<bool> Update(StudentDto peopleDto)
        {
            try
            {
                if (string.IsNullOrEmpty(peopleDto.Id))
                {
                    return Task.FromResult(false);
                }

                var database = _mongoClient.GetDatabase(Database);
                var studentCollection = database.GetCollection<Student>(Collection);

                var people = new Student()
                {
                    Id = peopleDto.Id!,
                    Name = peopleDto.Name,
                    Age = peopleDto.Age,
                    BirthDate = peopleDto.BirthDate
                };

                studentCollection.ReplaceOneAsync(u => u.Id == people.Id, people);
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
                var studentCollection = database.GetCollection<Student>(Collection);

                studentCollection.DeleteOneAsync(d => d.Id == id);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
    }
}
