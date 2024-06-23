using AutoMapper;
using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.CosmoDB;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interface;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeBasicDetailsService : IEmployeeBasicDetailsService
    {
        private readonly ICosmoDBService _cosmoDBService;
        private readonly IMapper _autoMapper;

        public EmployeeBasicDetailsService(ICosmoDBService cosmoDBService, IMapper mapper)
        {
            _cosmoDBService = cosmoDBService;
            _autoMapper = mapper;
        }

        public async Task<EmployeeBasicDetailsDTO> AddEmployeeBasicDetails(EmployeeBasicDetailsDTO employeeBasicDetails)
        {
            var entity = _autoMapper.Map<EmployeeBasicDetailsEntity>(employeeBasicDetails);
            entity.Intialize(true, "employeeBasicDetails", "Prerit", "Prerit");
            var response = await _cosmoDBService.Add(entity);
            return _autoMapper.Map<EmployeeBasicDetailsDTO>(response);
        }

        public async Task<IEnumerable<EmployeeBasicDetailsDTO>> GetAllEmployeeBasicDetails()
        {
            var entities = await _cosmoDBService.GetAllBasicDetails();
            return _autoMapper.Map<IEnumerable<EmployeeBasicDetailsDTO>>(entities);
        }

        public async Task<EmployeeBasicDetailsDTO> GetEmployeeBasicDetailsById(string id)
        {
            var entity = await _cosmoDBService.GetEmployeeBasicDetailsById(id);
            return _autoMapper.Map<EmployeeBasicDetailsDTO>(entity);
        }

        public async Task<EmployeeBasicDetailsDTO> UpdateEmployeeBasicDetails(string id, EmployeeBasicDetailsDTO employeeBasicDetails)
        {
            var entity = await _cosmoDBService.GetEmployeeBasicDetailsById(id);
            if (entity == null) throw new Exception("Employee not found");

            _autoMapper.Map(employeeBasicDetails, entity);
            entity.Intialize(false, "employeeBasicDetails", "System", "System");

            var response = await _cosmoDBService.Update(id, entity);
            return _autoMapper.Map<EmployeeBasicDetailsDTO>(response);
        }

        public async Task DeleteEmployeeBasicDetails(string id)
        {
            var entity = await _cosmoDBService.GetEmployeeBasicDetailsById(id);
            if (entity == null) throw new Exception("Employee not found");

            await _cosmoDBService.DeleteBasicDetails(id);
        }

        public async Task<EmployeeFilterCriteria> GetAllEmployeesByPagination(EmployeeFilterCriteria employeeFilterCriteria)
        {
            EmployeeFilterCriteria response = new EmployeeFilterCriteria();

            // Check for role filter
            var checkFilter = employeeFilterCriteria.Filters.Any(x => x.fieldName == "role");
            var role = "";
            if (checkFilter)
            {
                role = employeeFilterCriteria.Filters.Find(a => a.fieldName == "role").fieldValue.FirstOrDefault();
            }

            // Fetch all employee details
            var employees = await GetAllEmployeeBasicDetails();

            // Filter records based on role
            var filterRecords = string.IsNullOrEmpty(role) ? employees : employees.Where(a => a.Role == role);

            // Convert to list to ensure countable collection
            var employeeList = filterRecords.ToList();

            // Set total count
            response.totalCount = employeeList.Count;

            // Set pagination properties
            response.page = employeeFilterCriteria.page;
            response.pageSize = employeeFilterCriteria.pageSize;

            // Ensure valid page and pageSize
            if (response.page < 1) response.page = 1;
            if (response.pageSize < 1) response.pageSize = 10; // Default page size if not provided

            // Calculate skip
            var skip = response.pageSize * (response.page - 1);

            // Paginate the list
            var pagedRecords = employeeList.Skip(skip).Take(response.pageSize).ToList();

            // Add paginated records to response
            response.Employees = pagedRecords;

            return response;
        }

        public async Task<EmployeeBasicDetailsDTO> AddVisitorByMakePostRequest(EmployeeBasicDetailsDTO visitor)
        {
            var serialObj = JsonConvert.SerializeObject(visitor);
            var requestObj = await HttpClientHelper.MakePostRequest(Credentials.EmployeeUrl, Credentials.AddEmployeeEndPoint, serialObj);
            var responseObj = JsonConvert.DeserializeObject<EmployeeBasicDetailsDTO>(requestObj);
            return responseObj;

        }

     
        public async Task<IEnumerable<EmployeeBasicDetailsDTO>> GetAllEmployees()
        {
            var responseString = await HttpClientHelper.MakeGetRequest(Credentials.EmployeeUrl, Credentials.GetAllEmployeesEndPoint);
            var employees = JsonConvert.DeserializeObject<IEnumerable<EmployeeBasicDetailsDTO>>(responseString);
            return employees;
        }

        
    }

}
