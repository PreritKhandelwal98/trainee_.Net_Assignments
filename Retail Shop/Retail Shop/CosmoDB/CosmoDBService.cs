using Retail_Shop.Entities;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace Retail_Shop.CosmoDB
{
    public class CosmoDBService : ICosmoDBService
    {
        private readonly Container _container;

        public CosmoDBService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<RetailShopEntity> AddRetailShop(RetailShopEntity retailShopEntity)
        {
            var response = await _container.CreateItemAsync(retailShopEntity, new PartitionKey(retailShopEntity.UId));
            return response.Resource;
        }

        public async Task<RetailShopEntity> UpdateRetailShop(RetailShopEntity retailShopEntity)
        {
            var response = await _container.ReplaceItemAsync(retailShopEntity, retailShopEntity.Id, new PartitionKey(retailShopEntity.UId));
            return response.Resource;
        }

        public async Task<RetailShopEntity> GetRetailShopById(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<RetailShopEntity>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
