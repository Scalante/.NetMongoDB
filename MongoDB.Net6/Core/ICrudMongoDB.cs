using MongoDB.Net6.Model.Dtos;
using MongoDB.Net6.Model.Entities;

namespace MongoDB.Net6.Core
{
    public interface ICrudMongoDB
    {
        Task<List<People>> List();
        Task<bool> Insert(PeopleDto peopleDto);
        Task<bool> Update(PeopleDto peopleDto);
        Task<bool> Delete(string id);
    }
}
