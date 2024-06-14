using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.Entities;
using Microsoft.Azure.Cosmos.Linq;

namespace EmployeeManagementSystem.CosmoDB
{
    public class CosmoDBService : ICosmoDBService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public CosmoDBService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmoDBUrl, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.DatabaseName, Credentials.ContainerName);
        }

        public async Task<T> Add<T>(T entity)
        {
            var response = await _container.CreateItemAsync(entity);
            return response.Resource;
        }

        public async Task<T> Update<T>(string id, T entity)
        {
            var response = await _container.ReplaceItemAsync(entity, id);
            return response.Resource;
        }

        /*public async Task<IEnumerable<T>> GetAll<T>()
        {
            var query = _container.GetItemQueryIterator<T>();
            var results = new List<T>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }*/

        //Basic Details functions
        public async Task<EmployeeBasicDetailsEntity> GetEmployeeBasicDetailsById(string id)
        {
            try
            {
                var query = _container.GetItemLinqQueryable<EmployeeBasicDetailsEntity>(true)
                                      .Where(q => q.EmployeeID == id && q.Active && !q.Archived)
                                      .FirstOrDefault();
                return query;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
        public async Task DeleteBasicDetails(string id)
        {
            var employee = await GetEmployeeBasicDetailsById(id);
            if (employee != null)
            {
                employee.Active = false;
                employee.Archived = true;
                await Update(id, employee);
            }
            else
            {
                throw new Exception($"Item with ID {id} not found.");
            }
        }
        public async Task<IEnumerable<EmployeeBasicDetailsEntity>> GetAllBasicDetails()
        {
            var query = _container.GetItemLinqQueryable<EmployeeBasicDetailsEntity>(true)
                                  .Where(s => s.DocumentType == "employeeBasicDetails" && s.Active && !s.Archived)
                                  .AsQueryable();

            var iterator = query.ToFeedIterator();
            var results = new List<EmployeeBasicDetailsEntity>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }

        //Additional Details functions
        public async Task<EmployeeAdditionalDetailsEntity> GetEmployeeAdditionalDetailsById(string id)
        {
            try
            {
                var query = _container.GetItemLinqQueryable<EmployeeAdditionalDetailsEntity>(true)
                                      .Where(q => q.EmployeeBasicDetailsUId == id && q.Active && !q.Archived)
                                      .FirstOrDefault();
                return query;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
        public async Task DeleteAdditionalDetails(string id)
        {
            var employee = await GetEmployeeAdditionalDetailsById(id);
            if (employee != null)
            {
                employee.Active = false;
                employee.Archived = true;
                await Update(id, employee);
            }
            else
            {
                throw new Exception($"Item with ID {id} not found.");
            }
        }
        public async Task<IEnumerable<EmployeeAdditionalDetailsEntity>> GetAllAdditionalDetails()
        {
            var query = _container.GetItemLinqQueryable<EmployeeAdditionalDetailsEntity>(true)
                                  .Where(s => s.DocumentType == "employeeAdditionalDetails" && s.Active && !s.Archived)
                                  .AsQueryable();

            var iterator = query.ToFeedIterator();
            var results = new List<EmployeeAdditionalDetailsEntity>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }
    }
}
