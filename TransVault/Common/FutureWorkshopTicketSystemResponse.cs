using System.Net;

namespace FutureWorkshopTicketSystem.Common
{
    public class FutureWorkshopTicketSystemResponse<T> where T : class
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public T? Result { get; set; }
        public string? Message { get; set; }
        public string? InternalError { get; set; }
    }
}
