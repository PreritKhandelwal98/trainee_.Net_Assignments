using EmployeeManagementSystem.DTO;

namespace EmployeeManagementSystem.Interface
{
    public interface IEmployeeAdditionalDetailsService
    {
        Task<EmployeeAdditionalDetailsDTO> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsDTO employeeAdditionalDetails);
        Task<EmployeeAdditionalDetailsDTO> GetEmployeeAdditionalDetailsById(string id);
        Task<EmployeeAdditionalDetailsDTO> UpdateEmployeeAdditionalDetails(string id, EmployeeAdditionalDetailsDTO employeeAdditionalDetails);
        Task DeleteEmployeeAdditionalDetails(string id);
    }
}
