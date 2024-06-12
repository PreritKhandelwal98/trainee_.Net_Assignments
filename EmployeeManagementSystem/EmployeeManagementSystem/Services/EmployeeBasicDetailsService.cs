using AutoMapper;
using EmployeeManagementSystem.CosmoDB;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interface;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeBasicDetailsService: IEmployeeBasicDetailsService
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
            entity.Intialize(true, "employeeBasicDetails", "System", "System");
            var response = await _cosmoDBService.Add(entity);
            return _autoMapper.Map<EmployeeBasicDetailsDTO>(response);
        }

        public async Task<EmployeeBasicDetailsDTO> GetEmployeeBasicDetailsById(string id)
        {
            var entity = await _cosmoDBService.GetById<EmployeeBasicDetailsDTO>(id);
            return MapEntityToDTO(entity);
        }

        public async Task<EmployeeBasicDetailsDTO> UpdateEmployeeBasicDetails(string id, EmployeeBasicDetailsDTO employeeBasicDetails)
        {
            var entity = await _cosmoDBService.GetById<EmployeeBasicDetailsDTO>(id);
            if (entity == null) throw new Exception("Employee not found");

            // Update fields
            entity.Salutory = employeeBasicDetails.Salutory;
            entity.FirstName = employeeBasicDetails.FirstName;
            entity.MiddleName = employeeBasicDetails.MiddleName;
            entity.LastName = employeeBasicDetails.LastName;
            entity.NickName = employeeBasicDetails.NickName;
            entity.Email = employeeBasicDetails.Email;
            entity.Mobile = employeeBasicDetails.Mobile;
            entity.EmployeeID = employeeBasicDetails.EmployeeID;
            entity.Role = employeeBasicDetails.Role;
            entity.ReportingManagerUId = employeeBasicDetails.ReportingManagerUId;
            entity.ReportingManagerName = employeeBasicDetails.ReportingManagerName;
            entity.Address = employeeBasicDetails.Address;

            entity.Intialize(false, "employeeBasicDetails", "System", "System");

            var response = await _cosmoDBService.Update(entity, id);
            return MapEntityToDTO(response);
        }

        public async Task DeleteEmployeeBasicDetails(string id)
        {
            var entity = await _cosmoDBService.GetById<EmployeeBasicDetails>(id);
            if (entity == null) throw new Exception("Employee not found");

            await _cosmoDBService.Delete<EmployeeBasicDetails>(id);
        }

        private EmployeeBasicDetails MapDTOToEntity(EmployeeBasicDetailsDTO dto)
        {
            return new EmployeeBasicDetails
            {
                Id = dto.Id,
                Salutory = dto.Salutory,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                NickName = dto.NickName,
                Email = dto.Email,
                Mobile = dto.Mobile,
                EmployeeID = dto.EmployeeID,
                Role = dto.Role,
                ReportingManagerUId = dto.ReportingManagerUId,
                ReportingManagerName = dto.ReportingManagerName,
                Address = dto.Address
            };
        }

        private EmployeeBasicDetailsDTO MapEntityToDTO(EmployeeBasicDetails entity)
        {
            return new EmployeeBasicDetailsDTO
            {
                Id = entity.Id,
                Salutory = entity.Salutory,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                NickName = entity.NickName,
                Email = entity.Email,
                Mobile = entity.Mobile,
                EmployeeID = entity.EmployeeID,
                Role = entity.Role,
                ReportingManagerUId = entity.ReportingManagerUId,
                ReportingManagerName = entity.ReportingManagerName,
                Address = entity.Address
            };
        }
}
