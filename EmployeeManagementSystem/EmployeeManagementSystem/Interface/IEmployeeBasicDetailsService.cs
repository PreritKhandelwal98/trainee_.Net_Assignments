using EmployeeManagementSystem.DTO;

namespace EmployeeManagementSystem.Interface
{
    public interface IEmployeeBasicDetailsService
    {
        Task<EmployeeBasicDetailsDTO> AddEmployeeBasicDetails(EmployeeBasicDetailsDTO employeeBasicDetails);
        Task<EmployeeBasicDetailsDTO> GetEmployeeBasicDetailsById(string id);
        Task<EmployeeBasicDetailsDTO> UpdateEmployeeBasicDetails(string id, EmployeeBasicDetailsDTO employeeBasicDetails);
        Task DeleteEmployeeBasicDetails(string id);
    }
}
