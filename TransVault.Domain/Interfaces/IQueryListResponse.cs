namespace FutureWorkshopTicketSystem.Domain.Interfaces
{
    public interface IQueryListResponse<T>
    {
        List<T> Items { get; set; }
        int TotalRecordCount { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
    }
}
