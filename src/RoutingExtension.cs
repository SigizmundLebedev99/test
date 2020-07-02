using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

using test.Logic;

namespace test{
    static class RoutingExtension{

        public static void ConfigureRoute(this IApplicationBuilder app){
            var procService = (ProcessingService)app.ApplicationServices
                .GetService(typeof(ProcessingService));

            app.UseEndpoints(endpoints=>{
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/index.html");
                    return Task.CompletedTask;
                });
                endpoints.MapPost("/handle", async context => {
                    var file = context.Request.Form.Files.FirstOrDefault();

                    if(file == null)
                        context.Response.StatusCode = 400;
                    else{ 
                        var result = await procService.ProcessData(file);
                        await context.Response.WriteAsync(
                            JsonConvert.SerializeObject(result,
                                new JsonSerializerSettings(){
                                ContractResolver = new CamelCasePropertyNamesContractResolver()}
                                ));
                    }
                });
            });
        }
    }
}