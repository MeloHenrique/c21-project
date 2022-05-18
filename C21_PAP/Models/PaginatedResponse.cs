namespace C21_PAP.Models;

public class PaginatedResponse<T>
{
    public T[] Documents { get; set; }
    public int Total { get; set; }
}