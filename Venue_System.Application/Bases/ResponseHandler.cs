namespace Venue_System.Application.Bases
{
    public class ResponseHandler
    {

        public ResponseHandler()
        {

        }
        public Response<T> Deleted<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = Message == null ? "Delete Successfully" : Message
            };
        }
        public Response<T> Success<T>(T entity, string Message = null, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = Message == null ? "Success" : Message,
                Meta = Meta
            };
        }
        public Response<T> Unauthorized<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = false,
                Message = Message == null ? "Unauthorized" : Message,
            };
        }
        public Response<T> BadRequest<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? "Bad Request" : Message
            };
        }


        public Response<T> UnprocessableEntity<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = Message == null ? "Unprocessable Entity" : Message
            };
        }

        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? "Not Found" : message
            };
        }

        public Response<T> Created<T>(T entity, string message = null, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = message == null ? "Creates Successfully" : message,
                Meta = Meta
            };
        }
        public Response<T> Forbidden<T>(string message = null, object Meta = null)
        {
            return new Response<T>()
            {
                Data = default,
                StatusCode = System.Net.HttpStatusCode.Forbidden,
                Succeeded = false,
                Message = message == null ? "Forbidden" : message,
                Meta = Meta
            };
        }
    }

}
