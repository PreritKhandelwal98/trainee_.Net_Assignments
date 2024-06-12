using EmployeeManagementSystem.Common;
using Microsoft.Azure.Cosmos;

namespace EmployeeManagementSystem.CosmoDB
{
    public class CosmoDBService:ICosmoDBService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Microsoft.Azure.Cosmos.Container _container;

        public CosmoDBService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmoDBUrl, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.DatabaseName, Credentials.ContainerName);
        }

        //Generic Function for All

        public async Task<T> Add<T>(T data)
        {
            var response = await _container.CreateItemAsync(data);
            return response.Resource;
        }
        public async Task<T> Update<T>(T data)
        {
            var response = await _container.UpsertItemAsync(data);
            return response.Resource;
        }
        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var query = _container.GetItemQueryIterator<T>();
            var results = new List<T>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }

        public Task<T> Delete<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById<T>(string id)
        {
            throw new NotImplementedException();
        }
    }
}
