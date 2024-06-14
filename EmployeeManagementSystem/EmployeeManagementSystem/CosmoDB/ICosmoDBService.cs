using EmployeeManagementSystem.Entities;

namespace EmployeeManagementSystem.CosmoDB
{
    public interface ICosmoDBService
    {
        //Generic Function for all services
        Task<T> Add<T>(T entity);
        Task<T> Update<T>(string id,T entity);

        //Additional Details Function
        Task DeleteAdditionalDetails(string id);
        Task<EmployeeAdditionalDetailsEntity> GetEmployeeAdditionalDetailsById(string id);
        Task<IEnumerable<EmployeeAdditionalDetailsEntity>> GetAllAdditionalDetails();


        //Baisc Details Function
        Task<EmployeeBasicDetailsEntity> GetEmployeeBasicDetailsById(string id);
        Task DeleteBasicDetails(string id);
        Task<IEnumerable<EmployeeBasicDetailsEntity>> GetAllBasicDetails();

    }
}
