using Retail_Shop.Entities;

namespace Retail_Shop.CosmoDB
{
    public interface ICosmoDBService
    {
        Task<RetailShopEntity> AddRetailShop(RetailShopEntity retailShopEntity);
        Task<RetailShopEntity> UpdateRetailShop(RetailShopEntity retailShopEntity);
        Task<RetailShopEntity> GetRetailShopById(string id);
    }
}
