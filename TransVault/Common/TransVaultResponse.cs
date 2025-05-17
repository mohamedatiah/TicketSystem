using System.Net;

namespace TransVault.Common
{
    public class TransVaultResponse<T> where T : class
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public T? Result { get; set; }
        public string? Message { get; set; }
        public string? InternalError { get; set; }
    }
}
