using MudBlazor;

namespace C21_PAP.Models;

public class FilterOptions
{
    public int Page { get; set; }

    public int PageSize { get; set; }

    public string SortLabel { get; set; }

    public SortDirection SortDirection { get; set; }
    
    public string Search { get; set; }
}