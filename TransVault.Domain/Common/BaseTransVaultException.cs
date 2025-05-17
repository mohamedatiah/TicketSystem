namespace FutureWorkshopTicketSystem.Domain.Common
{
    public class BaseFutureWorkshopTicketSystemException : Exception
    {

        public BaseFutureWorkshopTicketSystemException(string message) : base(message) { }
        public BaseFutureWorkshopTicketSystemException(string message, Exception innerException) : base(message, innerException) { }
    }
}
