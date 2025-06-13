using System;
using System.Net;

namespace AdviLaw.Application.Basics
{
    public class ResponseHandler
    {
        public ResponseHandler() { }

       
        public Response<T> Success<T>(T data, object? meta = null)
        {
            return new Response<T>
            {
                Data = data,
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = "Operation successful",
                Meta = meta ?? new { timestamp = DateTime.UtcNow }
            };
        }

   
        public Response<T> Created<T>(T data, object? meta = null)
        {
            return new Response<T>
            {
                Data = data,
                StatusCode = HttpStatusCode.Created,
                Succeeded = true,
                Message = "Created successfully",
                Meta = meta ?? new { timestamp = DateTime.UtcNow }
            };
        }

     
        public Response<T> Deleted<T>()
        {
            return new Response<T>
            {
                StatusCode = HttpStatusCode.NoContent,
                Succeeded = true,
                Message = "Deleted successfully"
            };
        }

       
        public Response<T> NotFound<T>(string? message = null)
        {
            return new Response<T>
            {
                StatusCode = HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message ?? "Not found"
            };
        }

      
        public Response<T> BadRequest<T>(string? message = null)
        {
            return new Response<T>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = message ?? "Bad request"
            };
        }

     
        public Response<T> Unauthorized<T>(string? message = null)
        {
            return new Response<T>
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Succeeded = false,
                Message = message ?? "Unauthorized"
            };
        }

      
        public Response<T> UnprocessableEntity<T>(string? message = null)
        {
            return new Response<T>
            {
                StatusCode = HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = message ?? "Unprocessable entity"
            };
        }
    }
}
