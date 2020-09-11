# ng101s2notes

# Author Model
```
public class Author
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("picture")]
        public string Picture { get; set; }

        [Newtonsoft.Json.JsonProperty("age")]
        public int Age { get; set; }

        [Newtonsoft.Json.JsonProperty("genre")]
        public string Genre { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("gender")]
        public string Gender { get; set; }

        [Newtonsoft.Json.JsonProperty("email")]
        public string Email { get; set; }

        [Newtonsoft.Json.JsonProperty("phone")]
        public string Phone { get; set; }

        [Newtonsoft.Json.JsonProperty("authorBio")]
        public string AuthorBio { get; set; }

        [Newtonsoft.Json.JsonProperty("publishDate")]
        public string PublishDate { get; set; }

        [Newtonsoft.Json.JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }
 ```
# Install Nuget Packages
* Install-Package Microsoft.Azure.Cosmos -Version 3.12.0
* Install-Package Swashbuckle.AspNetCore -Version 5.5.0


# IRepository

``` 
interface IRepository<T> where T: class
    {
        Task<List<T>> GetAllAsync(string partition);

        Task<T> GetByIdAsync(string id, string partition);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

    }
    
```

# Author Repository - Implement the interface

```

public class AuthorRepository : IRepository<Author>
    {

        private const string CONN_STRING = @"";

        private CosmosClient _client;
        private Container _container;
        private string _database;
        private string _containerName;
        private string _partition_key;

        public AuthorRepository()
        {
            _database = "LanallaiPublications";
            _containerName = "Authors";
            _partition_key = "/genre";
            _client = new CosmosClient(CONN_STRING);
            InitialiseCosmos().Wait();
        }

        private async Task InitialiseCosmos()
        {
            var databaseCreateResponse = await _client.CreateDatabaseIfNotExistsAsync(_database, 400);
            if(databaseCreateResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var containerCreateResponse = await databaseCreateResponse.Database.CreateContainerIfNotExistsAsync(_containerName, _partition_key);
                _container = containerCreateResponse.Container;
            }
            else
            {
                _container = databaseCreateResponse.Database.GetContainer(_containerName);
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
            using(FeedIterator<Author> result = _container.GetItemQueryIterator<Author>(
                queryDefinition: null
                , requestOptions: new QueryRequestOptions() { PartitionKey = new PartitionKey(partition) }
                ))
            {
                while(result.HasMoreResults)
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
            var result =await _container.UpsertItemAsync(entity, partitionKey);
            return result;
        }
    }
 ```
 
 
 # Authors Controller
 
 ```
   [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorRepository _authorRepository;

        public AuthorsController()
        {
            _authorRepository = new AuthorRepository();
        }

        [HttpGet]
        [Route("{genre}/{id}")]
        public async Task<IActionResult> GetAuthorByGenre([FromRoute] string genre, [FromRoute] string id)
        {
            var result = await _authorRepository.GetByIdAsync(id, genre);
            return Ok(result);
        }

        [HttpGet]
        [Route("{genre}")]
        public async Task<IActionResult> GetAuthorsByGenre([FromRoute] string genre)
        {
            var result = await _authorRepository.GetAllAsync(genre);
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAuthor([FromBody] Author author)
        {
            var result = await _authorRepository.UpdateAsync(author);
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] Author author)
        {
            var result = await _authorRepository.CreateAsync(author);
            return Ok(result);
        }

        [HttpPost]
        [Route("bulk-load")]
        public async Task<IActionResult> BulkLoadAuthors([FromBody] List<Author> authors)
        {
            foreach (var author in authors)
            {
                await _authorRepository.CreateAsync(author);
            }
            return Ok("Created");
        }
    }


```
 
# Swagger - Startup.cs
```
 public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    
    
```
