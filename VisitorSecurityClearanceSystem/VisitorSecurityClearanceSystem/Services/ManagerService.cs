using VisitorSecurityClearanceSystem.CosmoDB;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Entites;
using VisitorSecurityClearanceSystem.Interface;

namespace VisitorSecurityClearanceSystem.Services
{
    public class ManagerService:IManagerService
    {
        private readonly ICosmoDBService _cosmoDBService;

        public ManagerService(ICosmoDBService cosmoDBService)
        {
            _cosmoDBService = cosmoDBService;
        }

        public async Task<ManagerDTO> AddManager(ManagerDTO managerModel)
        {

            // Map the DTO to an Entity
            var managerEntity = MapDTOToEntity(managerModel);

            // Initialize the Entity
            managerEntity.Intialize(true, "manager", "Prerit", "Prerit");

            // Add the entity to the database
            var response = await _cosmoDBService.Add(managerEntity);

            // Map the response back to a DTO
            return MapEntityToDTO(response);
        }

        public async Task<ManagerDTO> GetManagerById(string id)
        {
            var security = await _cosmoDBService.GetManagerById(id); // Call non-generic method
            return MapEntityToDTO(security);
        }

        public async Task<ManagerDTO> UpdateManager(string id, ManagerDTO managerModel)
        {
            var managerEntity = await _cosmoDBService.GetManagerById(id);
            if (managerEntity == null)
            {
                throw new Exception("Manager not found");
            }
            managerEntity = MapDTOToEntity(managerModel);
            managerEntity.Id = id;
            var response = await _cosmoDBService.Update(managerEntity);
            return MapEntityToDTO(response);
        }

        /*public async Task DeleteManager(string id)
        {
            await _cosmoDBService.Delete<ManagerEntity>(id);
        }*/

        private ManagerEntity MapDTOToEntity(ManagerDTO managerModel)
        {
            return new ManagerEntity
            {
                Id = managerModel.Id,
                Name = managerModel.Name,
                Email = managerModel.Email,
                Phone = managerModel.Phone,
                Role = managerModel.Role,
                
            };
        }

        private ManagerDTO MapEntityToDTO(ManagerEntity managerEntity)
        {
            if (managerEntity == null) return null;
            return new ManagerDTO
            {
                Id = managerEntity.Id,
                Name = managerEntity.Name,
                Email = managerEntity.Email,
                Phone = managerEntity.Phone,
                Role = managerEntity.Role,
                
            };
        }
    }
}
