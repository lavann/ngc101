using AuthorsApi.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsApi.Repos
{
    public class AuthorRepository : IRepository<Author>
    {

        private const string CONN_STRING = @"AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        private CosmosClient _cosmosClient;
        private Container _container;
        private string _database;
        private string _partition_key; //add ink to md for cosmos db partition strategy
        private string _containerName;

        public AuthorRepository() //solid 
        {
            _database = "lanallaiPublications";
            _containerName = "Authors";
            _partition_key = "/genre";
            _cosmosClient = new CosmosClient(CONN_STRING);
            InitliaseCosmos().Wait();
        }

        private async Task InitliaseCosmos()
        {
            var databaseCreationResponse =await  _cosmosClient.CreateDatabaseIfNotExistsAsync(_database, 400);

            if(databaseCreationResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var containerCreateResponse = await databaseCreationResponse.Database.CreateContainerIfNotExistsAsync(_containerName, _partition_key);
                _container = containerCreateResponse.Container;
            }
            else
            {
                _container = databaseCreationResponse.Database.GetContainer(_containerName); // app settings and DI 
            }
        }


        public async Task<Author> CreateAsync(Author entity)
        {
            var result = await _container.CreateItemAsync<Author>(entity);

            return result;
        }

        public async Task<List<Author>> GetAllAsync(string partition)
        {
            List<Author> authors = new List<Author>();

            using (FeedIterator<Author> result = _container.GetItemQueryIterator<Author>(
                queryDefinition: null
                , requestOptions: new QueryRequestOptions() { PartitionKey = new PartitionKey(partition) }
                ))
            {
                while (result.HasMoreResults)
                {
                    FeedResponse<Author> response = await result.ReadNextAsync();
                    authors.AddRange(response);
                }
            }
            return authors;
        }

        public async Task<Author> GetByIdAsync(string id, string partition)
        {
            var partitionKey = new PartitionKey(partition);
            var result = await _container.ReadItemAsync<Author>(id, partitionKey);

            return result;
        }

        public async Task<Author> UpdateAsync(Author entity)
        {
            var partitionKey = new PartitionKey(entity.Genre);
            var result = await _container.UpsertItemAsync<Author>(entity, partitionKey);

            return result;
        }
    }
}
