using VisitorSecurityClearanceSystem.CosmoDB;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Interface;

namespace VisitorSecurityClearanceSystem.Services
{
    public class OfficeService:IOfficeService
    {
        private readonly ICosmoDBService _cosmoDBService;

        public OfficeService(ICosmoDBService cosmoDBService)
        {
            _cosmoDBService = cosmoDBService;
        }

        public async Task<OfficeDTO> AddOffice(OfficeDTO officeModel)
        {

            // Map the DTO to an Entity
            var officeEntity = MapDTOToEntity(officeModel);

            // Initialize the Entity
            officeEntity.Intialize(true, "security", "Prerit", "Prerit");

            // Add the entity to the database
            var response = await _cosmoDBService.Add(officeEntity);

            // Map the response back to a DTO
            return MapEntityToDTO(response);
        }

        public async Task<OfficeDTO> GetOfficeById(string id)
        {
            var office = await _cosmoDBService.GetOfficeById(id); // Call non-generic method
            return MapEntityToDTO(office);
        }

        public async Task<OfficeDTO> UpdateOffice(string id, OfficeDTO officeModel)
        {
            var officeEntity = await _cosmoDBService.GetOfficeById(id);
            if (officeEntity == null)
            {
                throw new Exception("Office not found");
            }
            officeEntity = MapDTOToEntity(officeModel);
            officeEntity.Id = id;
            var response = await _cosmoDBService.Update(officeEntity);
            return MapEntityToDTO(response);
        }

        public async Task DeleteOffice(string id)
        {
            await _cosmoDBService.Delete<SecurityEntity>(id);
        }

        private OfficeEntity MapDTOToEntity(OfficeDTO officeModel)
        {
            return new OfficeEntity
            {
                Id = officeModel.Id,
                Name = officeModel.Name,
                Email = officeModel.Email,
                Phone = officeModel.Phone,
                Role = "Office"
            };
        }

        private OfficeDTO MapEntityToDTO(OfficeEntity officeEntity)
        {
            if (officeEntity == null) return null;
            return new OfficeDTO
            {
                Id = officeEntity.Id,
                Name = officeEntity.Name,
                Email = officeEntity.Email,
                Phone = officeEntity.Phone,
            };
        }
    }
}
