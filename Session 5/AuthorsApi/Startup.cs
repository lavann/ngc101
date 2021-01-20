using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AuthorsApi
{
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
            //services.AddControllers(); -- causes the following error

            //            System.NotSupportedException: The collection type 'Newtonsoft.Json.Linq.JObject' is not supported.
            //   at System.Text.Json.JsonPropertyInfoNotNullable`4.GetDictionaryKeyAndValueFromGenericDictionary(WriteStackFrame & writeStackFrame, String & key, Object & value)
            //   at System.Text.Json.JsonPropertyInfo.GetDictionaryKeyAndValue(WriteStackFrame & writeStackFrame, String & key, Object & value)
            //   at System.Text.Json.JsonSerializer.HandleDictionary(JsonClassInfo elementClassInfo, JsonSerializerOptions options, Utf8JsonWriter writer, WriteStack & state)
            //   at System.Text.Json.JsonSerializer.Write(Utf8JsonWriter writer, Int32 originalWriterDepth, Int32 flushThreshold, JsonSerializerOptions options, WriteStack & state)
            //   at System.Text.Json.JsonSerializer.WriteAsyncCore(Stream utf8Json, Object value, Type inputType, JsonSerializerOptions options, CancellationToken cancellationToken)
            //   at Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter.WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
            //   at Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter.WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
            //   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.< InvokeNextResultFilterAsync > g__Awaited | 29_0[TFilter, TFilterAsync](ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
            //   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Rethrow(ResultExecutedContextSealed context)
            //   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.ResultNext[TFilter, TFilterAsync](State & next, Scope & scope, Object & state, Boolean & isCompleted)
            //   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.InvokeResultFilters()
            //-- - End of stack trace from previous location where exception was thrown ---
            //    at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.< InvokeFilterPipelineAsync > g__Awaited | 19_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
            //   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.< InvokeAsync > g__Awaited | 17_0(ResourceInvoker invoker, Task task, IDisposable scope)
            //   at Microsoft.AspNetCore.Routing.EndpointMiddleware.< Invoke > g__AwaitRequestTask | 6_0(Endpoint endpoint, Task requestTask, ILogger logger)
            //   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
            //   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
            //   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
            //   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware.Invoke(HttpContext context)
           
            // seems to be the fix
            
            //https://dotnetcoretutorials.com/2019/12/19/using-newtonsoft-json-in-net-core-3-projects/

            services.AddControllers().AddNewtonsoftJson();

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
            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();

            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
