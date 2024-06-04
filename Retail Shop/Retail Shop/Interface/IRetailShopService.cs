using Retail_Shop.DTO;
using Retail_Shop.Entities;

namespace Retail_Shop.Interface
{
    public interface IRetailShopService
    {
        Task<RetailShopDTO> CreateRetailShop(RetailShopDTO retailShopDTO);
        Task<RetailShopDTO> UpdateRetailShop(string id, RetailShopDTO retailShopDTO);
        Task<RetailShopEntity> GetRetailShopById(string id);
    }
}
