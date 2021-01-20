using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsApi.Repos
{
    public class GenericRepo<T> : IRepositoryOfT<T> where T: class
    {

        private const string CONN_STRING = @"AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        private CosmosClient _cosmosClient;
        private Container _container;
        private string _database;
        private string _partition_key; //add ink to md for cosmos db partition strategy
        private string _containerName;

        public GenericRepo()
        {
            _database = "lanallaiPublications";
            _containerName = "Authors";
            _partition_key = "/genre";
            _cosmosClient = new CosmosClient(CONN_STRING);
            InitliaseCosmos().Wait();
        }

        private async Task InitliaseCosmos()
        {
            var databaseCreationResponse = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_database, 400);

            if (databaseCreationResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var containerCreateResponse = await databaseCreationResponse.Database.CreateContainerIfNotExistsAsync(_containerName, _partition_key);
                _container = containerCreateResponse.Container;
            }
            else
            {
                _container = databaseCreationResponse.Database.GetContainer(_containerName); // app settings and DI 
            }
        }

        public async Task<T> CreateAsync(T entity)
        {
            var result = await _container.CreateItemAsync<T>(entity);

            return result;
        }

        public async Task<List<T>> GetAllAsync(string partition)
        {
            List<T> responseList = new List<T>();

            using (FeedIterator<T> result = _container.GetItemQueryIterator<T>(
                queryDefinition: null
                , requestOptions: new QueryRequestOptions() { PartitionKey = new PartitionKey(partition) }
                ))
            {
                while (result.HasMoreResults)
                {
                    FeedResponse<T> response = await result.ReadNextAsync();
                    responseList.AddRange(response);
                }
            }
            return responseList;
        }

        public async Task<T> GetByIdAsync(string id, string partition)
        {
            var partitionKey = new PartitionKey(partition);
            var result = await _container.ReadItemAsync<T>(id, partitionKey);

            return result;
        }

        public async Task<T> UpdateAsync(T entity, string partition)
        {
            var partitionKey = new PartitionKey(partition);
            var result = await _container.UpsertItemAsync<T>(entity, partitionKey);

            return result;
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
