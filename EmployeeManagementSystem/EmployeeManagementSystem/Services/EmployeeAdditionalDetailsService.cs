using AutoMapper;
using EmployeeManagementSystem.CosmoDB;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeAdditionalDetailsService : IEmployeeAdditionalDetailsService
    {
        private readonly ICosmoDBService _cosmoDBService;
        private readonly IMapper _autoMapper;
        private readonly IEmployeeBasicDetailsService _basicDetailsService;

        public EmployeeAdditionalDetailsService(ICosmoDBService cosmoDBService, IMapper mapper, IEmployeeBasicDetailsService basicDetailsService)
        {
            _cosmoDBService = cosmoDBService;
            _autoMapper = mapper;
            _basicDetailsService = basicDetailsService;
        }

        public async Task<EmployeeAdditionalDetailsDTO> GetEmployeeAdditionalDetailsById(string id)
        {
            var entity = await _cosmoDBService.GetEmployeeAdditionalDetailsById(id);
            return _autoMapper.Map<EmployeeAdditionalDetailsDTO>(entity);
        }

        public async Task<EmployeeAdditionalDetailsDTO> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsDTO additionalDetailsDTO)
        {
            // Fetch the basic details using the provided EmployeeBasicDetailsUId
            var basicDetails = await _basicDetailsService.GetEmployeeBasicDetailsById(additionalDetailsDTO.EmployeeBasicDetailsUId);

            // Check if the basic details exist
            if (basicDetails == null)
            {
                throw new Exception("Basic details not found for the provided ID");
            }

            // Proceed with adding additional details
            var entity = _autoMapper.Map<EmployeeAdditionalDetailsEntity>(additionalDetailsDTO);
/*            entity.Initialize(true, "employeeAdditionalDetails", "Prerit", "Prerit");
*/            var response = await _cosmoDBService.Add(entity);
            return _autoMapper.Map<EmployeeAdditionalDetailsDTO>(response);
        }






        public async Task<EmployeeAdditionalDetailsDTO> UpdateEmployeeAdditionalDetails(string id, EmployeeAdditionalDetailsDTO additionalDetailsDTO)
        {
            var entity = await _cosmoDBService.GetEmployeeAdditionalDetailsById(id);
            if (entity == null) throw new Exception("Employee not found");

            // Map the updated details from DTO to the existing entity
            _autoMapper.Map(additionalDetailsDTO, entity);
            entity.Intialize(false, "employeeAdditionalDetails", "System", "System");

            var response = await _cosmoDBService.Update(id, entity);
            return _autoMapper.Map<EmployeeAdditionalDetailsDTO>(response);
        }

        public async Task DeleteEmployeeAdditionalDetails(string id)
        {
            var entity = await _cosmoDBService.GetEmployeeAdditionalDetailsById(id);
            if (entity == null) throw new Exception("Employee not found");

            await _cosmoDBService.DeleteAdditionalDetails(id);
        }

        public async Task<IEnumerable<EmployeeAdditionalDetailsDTO>> GetAllEmployeeAdditionalDetails()
        {
            var entities = await _cosmoDBService.GetAllAdditionalDetails();
            return _autoMapper.Map<IEnumerable<EmployeeAdditionalDetailsDTO>>(entities);
        }
    }
}
