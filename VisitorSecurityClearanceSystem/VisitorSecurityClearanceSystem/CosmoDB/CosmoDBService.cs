using System.ComponentModel;
using VisitorSecurityClearanceSystem.Common;
using VisitorSecurityClearanceSystem.Entites;

namespace VisitorSecurityClearanceSystem.CosmoDB
{
    public class CosmoDBService:ICosmoDBService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public CosmoDBService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndpoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.DatabaseName, Credentials.ContainerName);
        }

        public async Task<T> Add<T>(T data)
        {
            var response = await _container.CreateItemAsync(data);
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

        public async Task<T> Update<T>(T data)
        {
            var response = await _container.UpsertItemAsync(data);
            return response.Resource;
        }

        public async Task Delete<T>(string id)
        {
            await _container.DeleteItem<T>(id.ToString());
        }

        public async Task<VisitorEntity> GetVisitorByEmail(string email)
        {
            var query = _container.GetItemLinqQueryable<VisitorEntity>(true)
                                  .Where(q => q.Email == email && q.Active && !q.Archived)
                                  .ToFeedIterator();

            if (query.HasMoreResults)
            {
                var resultSet = await query.ReadNextAsync();
                return resultSet.FirstOrDefault();
            }

            return null;
        }

        public Task<T> GetById<T>(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<VisitorEntity> GetVisitorById(string id)
        {
            try
            {
                var query = _container.GetItemLinqQueryable<VisitorEntity>(true)
                                      .Where(q => q.Id == id && q.Active && !q.Archived)
                                      .ToFeedIterator();

                while (query.HasMoreResults)
                {
                    var resultSet = await query.ReadNextAsync();
                    var visitor = resultSet.FirstOrDefault();
                    if (visitor != null)
                    {
                        return visitor;
                    }
                }

                return null;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<SecurityEntity> GetSecurityById(string id)
        {
            try
            {
                var query = _container.GetItemLinqQueryable<SecurityEntity>(true)
                                      .Where(q => q.Id == id && q.Active && !q.Archived)
                                      .ToFeedIterator();

                while (query.HasMoreResults)
                {
                    var resultSet = await query.ReadNextAsync();
                    var security = resultSet.FirstOrDefault();
                    if (security != null)
                    {
                        return security;
                    }
                }

                return null;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
        public async Task<ManagerEntity> GetManagerById(string id)
        {
            try
            {
                var query = _container.GetItemLinqQueryable<ManagerEntity>(true)
                                      .Where(q => q.Id == id && q.Active && !q.Archived)
                                      .ToFeedIterator();

                while (query.HasMoreResults)
                {
                    var resultSet = await query.ReadNextAsync();
                    var manager = resultSet.FirstOrDefault();
                    if (manager != null)
                    {
                        return manager;
                    }
                }

                return null;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<OfficeEntity> GetOfficeById(string id)
        {
            try
            {
                var query = _container.GetItemLinqQueryable<OfficeEntity>(true)
                                      .Where(q => q.Id == id && q.Active && !q.Archived)
                                      .ToFeedIterator();

                while (query.HasMoreResults)
                {
                    var resultSet = await query.ReadNextAsync();
                    var office = resultSet.FirstOrDefault();
                    if (office != null)
                    {
                        return office;
                    }
                }

                return null;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
