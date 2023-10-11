using MongoDB.Net6.Model.Dtos;
using MongoDB.Net6.Model.Entities;

namespace MongoDB.Net6.Core
{
    public interface ICrudMongoDB
    {
        Task<List<Student>> List();
        Task<bool> Insert(StudentDto peopleDto);
        Task<bool> Update(StudentDto peopleDto);
        Task<bool> Delete(string id);
    }
}
