using System.Net;

namespace AdviLaw.Application.Basics
{
    public class ResponseHandler
    {
        public ResponseHandler()
        {
        }

        public Response<object> Deleted()
        {
            return new Response<object>
            {
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = "Deleted successfully"
            };
        }

        public Response<object> Success(object data, object meta = null)
        {
            return new Response<object>
            {
                Data = data,
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = "Operation successful",
                Meta = new { timestamp = DateTime.UtcNow }
            };
        }

        public Response<object> Unauthorized(string message = null)
        {
            return new Response<object>
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Succeeded = false,
                Message = message ?? "Unauthorized"
            };
        }

        public Response<object> BadRequest(string message = null)
        {
            return new Response<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = message ?? "Bad request"
            };
        }

        public Response<object> UnprocessableEntity(string message = null)
        {
            return new Response<object>
            {
                StatusCode = HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = message ?? "Unprocessable entity"
            };
        }

        public Response<object> NotFound(string message = null)
        {
            return new Response<object>
            {
                StatusCode = HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message ?? "Not found"
            };
        }

        public Response<object> Created(object data, object meta = null)
        {
            return new Response<object>
            {
                Data = data,
                StatusCode = HttpStatusCode.Created,
                Succeeded = true,
                Message = "Created successfully",
                Meta = new { timestamp = DateTime.UtcNow }
            };
        }
    }
}
