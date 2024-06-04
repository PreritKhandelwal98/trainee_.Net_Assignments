using Retail_Shop.CosmoDB;
using Retail_Shop.DTO;
using Retail_Shop.Entities;
using Retail_Shop.Interface;

namespace Retail_Shop.Services
{
    public class RetailShopService : IRetailShopService
    {
        private readonly ICosmoDBService _cosmoDBService;

        public RetailShopService(ICosmoDBService cosmoDBService)
        {
            _cosmoDBService = cosmoDBService;
        }

        public async Task<RetailShopDTO> CreateRetailShop(RetailShopDTO retailShopDTO)
        {
            var retailShopEntity = new RetailShopEntity
            {
                Name = retailShopDTO.Name,
                Location = retailShopDTO.Location,
                OwnerName = retailShopDTO.OwnerName,
                ContactNumber = retailShopDTO.ContactNumber
            };
            retailShopEntity.Intialize(true, "retailShop", "system", "system");

            var response = await _cosmoDBService.AddRetailShop(retailShopEntity);

            return new RetailShopDTO
            {
                UId = response.UId,
                Id = response.Id,
                Name = response.Name,
                Location = response.Location,
                OwnerName = response.OwnerName,
                ContactNumber = response.ContactNumber
            };
        }

        public async Task<RetailShopDTO> UpdateRetailShop(string id, RetailShopDTO retailShopDTO)
        {
            var retailShopEntity = await _cosmoDBService.GetRetailShopById(id);
            if (retailShopEntity == null)
            {
                throw new Exception("Retail shop not found");
            }

            retailShopEntity.Name = retailShopDTO.Name;
            retailShopEntity.Location = retailShopDTO.Location;
            retailShopEntity.OwnerName = retailShopDTO.OwnerName;
            retailShopEntity.ContactNumber = retailShopDTO.ContactNumber;

            var response = await _cosmoDBService.UpdateRetailShop(retailShopEntity);

            return new RetailShopDTO
            {
                UId = response.UId,
                Id = response.Id,
                Name = response.Name,
                Location = response.Location,
                OwnerName = response.OwnerName,
                ContactNumber = response.ContactNumber
            };
        }

        public async Task<RetailShopEntity> GetRetailShopById(string id)
        {
            return await _cosmoDBService.GetRetailShopById(id);
        }
    }
}
