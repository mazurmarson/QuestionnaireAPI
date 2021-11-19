using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using QuestionnaireAPI.Exceptions;

namespace QuestionnaireAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        // public ErrorHandlingMiddleware()
        // {
            
        // }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (ResourceDoesExistException resourceDoesExistException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(resourceDoesExistException.Message);
            }
            catch (ValidationException validationException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(validationException.Message);
            }
            catch(UnauthorizedException unauthorizedException)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(unauthorizedException.Message);
            }
            catch(Exception e)
            {
               System.Console.WriteLine(e);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}