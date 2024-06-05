using VisitorSecurityClearanceSystem.CosmoDB;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Interface;

namespace VisitorSecurityClearanceSystem.Services
{
    public class SecurityService:ISecurityService
    {
        private readonly ICosmoDBService _cosmoDBService;

        public SecurityService(ICosmoDBService cosmoDBService)
        {
            _cosmoDBService = cosmoDBService;
        }

        public async Task<SecurityDTO> AddSecurity(SecurityDTO securityModel)
        {

            // Map the DTO to an Entity
            var securityEntity = MapDTOToEntity(securityModel);

            // Initialize the Entity
            securityEntity.Intialize(true, "security", "Prerit", "Prerit");

            // Add the entity to the database
            var response = await _cosmoDBService.Add(securityEntity);

            // Map the response back to a DTO
            return MapEntityToDTO(response);
        }

        public async Task<SecurityDTO> GetSecurityById(string id)
        {
            var security = await _cosmoDBService.GetSecurityById(id); // Call non-generic method
            return MapEntityToDTO(security);
        }

        public async Task<SecurityDTO> UpdateSecurity(string id, SecurityDTO securityModel)
        {
            var securityEntity = await _cosmoDBService.GetSecurityById(id);
            if (securityEntity == null)
            {
                throw new Exception("Security not found");
            }
            securityEntity = MapDTOToEntity(securityModel);
            securityEntity.Id = id;
            var response = await _cosmoDBService.Update(securityEntity);
            return MapEntityToDTO(response);
        }

        public async Task DeleteSecurity(string id)
        {
            await _cosmoDBService.Delete<SecurityEntity>(id);
        }

        private SecurityEntity MapDTOToEntity(SecurityDTO securityModel)
        {
            return new SecurityEntity
            {
                Id = securityModel.Id,
                Name = securityModel.Name,
                Email = securityModel.Email,
                Phone = securityModel.Phone,
                Role = "security"
            };
        }

        private SecurityDTO MapEntityToDTO(SecurityEntity securityEntity)
        {
            if (securityEntity == null) return null;
            return new SecurityDTO
            {
                Id = securityEntity.Id,
                Name = securityEntity.Name,
                Email = securityEntity.Email,
                Phone = securityEntity.Phone,
                Role = "security"
            };
        }
    }
}
