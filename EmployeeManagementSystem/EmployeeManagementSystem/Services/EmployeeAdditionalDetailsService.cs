using EmployeeManagementSystem.CosmoDB;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Interface;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeAdditionalDetailsService: IEmployeeAdditionalDetailsService
    {
        private readonly ICosmoDBService _cosmoDBService;

        public EmployeeAdditionalDetailsService(ICosmoDBService cosmoDBService)
        {
            _cosmoDBService = cosmoDBService;
        }

        public async Task<EmployeeAdditionalDetailsDTO> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsDTO employeeAdditionalDetails)
        {
            var entity = MapDTOToEntity(employeeAdditionalDetails);
            entity.Intialize(true, "employeeAdditionalDetails", "System", "System");
            var response = await _cosmoDBService.Add(entity);
            return MapEntityToDTO(response);
        }

        public async Task<EmployeeAdditionalDetailsDTO> GetEmployeeAdditionalDetailsById(string id)
        {
            var entity = await _cosmoDBService.GetById<EmployeeAdditionalDetailsDTO>(id);
            return MapEntityToDTO(entity);
        }

        public async Task<EmployeeAdditionalDetailsDTO> UpdateEmployeeAdditionalDetails(string id, EmployeeAdditionalDetailsDTO employeeAdditionalDetails)
        {
            var entity = await _cosmoDBService.GetById<EmployeeAdditionalDetailsDTO>(id);
            if (entity == null) throw new Exception("Employee not found");

            // Update fields
            entity.EmployeeBasicDetailsUId = employeeAdditionalDetails.EmployeeBasicDetailsUId;
            entity.AlternateEmail = employeeAdditionalDetails.AlternateEmail;
            entity.AlternateMobile = employeeAdditionalDetails.AlternateMobile;
            entity.WorkInformation = employeeAdditionalDetails.WorkInformation;
            entity.PersonalDetails = employeeAdditionalDetails.PersonalDetails;
            entity.IdentityInformation = employeeAdditionalDetails.IdentityInformation;

            entity.Intialize(false, "employeeAdditionalDetails", "System", "System");

            var response = await _cosmoDBService.Update(entity, id);
            return MapEntityToDTO(response);
        }

        public async Task DeleteEmployeeAdditionalDetails(string id)
        {
            var entity = await _cosmoDBService.GetById<EmployeeAdditionalDetailsDTO>(id);
            if (entity == null) throw new Exception("Employee not found");

            await _cosmoDBService.Delete<EmployeeAdditionalDetailsDTO>(id);
        }

        private EmployeeAdditionalDetailsDTO MapDTOToEntity(EmployeeAdditionalDetailsDTO dto)
        {
            return new EmployeeAdditionalDetailsDTO
            {
                Id = dto.Id,
                EmployeeBasicDetailsUId = dto.EmployeeBasicDetailsUId,
                AlternateEmail = dto.AlternateEmail,
                AlternateMobile = dto.AlternateMobile,
                WorkInformation = dto.WorkInformation,
                PersonalDetails = dto.PersonalDetails,
                IdentityInformation = dto.IdentityInformation
            };
        }

        private EmployeeAdditionalDetailsDTO MapEntityToDTO(EmployeeAdditionalDetailsDTO entity)
        {
            return new EmployeeAdditionalDetailsDTO
            {
                Id = entity.Id,
                EmployeeBasicDetailsUId = entity.EmployeeBasicDetailsUId,
                AlternateEmail = entity.AlternateEmail,
                AlternateMobile = entity.AlternateMobile,
                WorkInformation = entity.WorkInformation,
                PersonalDetails = entity.PersonalDetails,
                IdentityInformation = entity.IdentityInformation
            };
        }
    }
}
