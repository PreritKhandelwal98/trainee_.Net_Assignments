namespace EmployeeManagementSystem.CosmoDB
{
    public interface ICosmoDBService
    {
        //Generic Function for all services
        Task<IEnumerable<T>> GetAll<T>();
        Task<T> Add<T>(T entity);
        Task<T> Update<T>(T entity);
        Task<T> Delete<T>(T entity);
        Task<T> GetById<T>(string id);
    }
}
