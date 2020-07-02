using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;

namespace test
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (InvalidOperationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (InvalidDataException ex){
                context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception){
                throw;
            }
        }
    }
}