namespace MongoDB.Net6.Core
{
    public partial interface ICrudMongoDB<TDocument>
    {
        Task<IQueryable<TDocument>> ListAsync();
        Task<bool> InsertAsync(TDocument model);
        Task<bool> UpdateAsync(TDocument model);
        Task<bool> DeleteAsync(string id);
    }
}
