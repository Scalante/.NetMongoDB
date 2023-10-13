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

            Task<List<Student>> listStudent = studentCollection.Find(d => true).ToListAsync();

            return listStudent;
        }

        public Task<bool> Insert(StudentDto studenDto)
        {
            try
            {
                var database = _mongoClient.GetDatabase(Database);
                var studentCollection = database.GetCollection<Student>(Collection);

                var student = new Student()
                {
                    Name = studenDto.Name,
                    Age = studenDto.Age,
                    BirthDate = studenDto.BirthDate
                };

                studentCollection.InsertOneAsync(student);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }


        public Task<bool> Update(StudentDto studenDto)
        {
            try
            {
                if (string.IsNullOrEmpty(studenDto.Id))
                {
                    return Task.FromResult(false);
                }

                var database = _mongoClient.GetDatabase(Database);
                var studentCollection = database.GetCollection<Student>(Collection);

                var people = new Student()
                {
                    Id = studenDto.Id!,
                    Name = studenDto.Name,
                    Age = studenDto.Age,
                    BirthDate = studenDto.BirthDate
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
