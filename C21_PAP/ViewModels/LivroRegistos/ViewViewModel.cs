using C21_PAP.Models;
using C21_PAP.Models.Process;
using C21_PAP.Pages.LivroRegistos;
using C21_PAP.Services;
using MudBlazor;
using MvvmBlazor.ViewModel;

namespace C21_PAP.ViewModels.LivroRegistos;

public class ViewViewModel : ViewModelBase
{
    readonly IDialogService DialogService;
    readonly ContractsService ContractsService;
    public MudTable<ProcessModel> Table;
    private string? SearchString = null;
    public bool isBusy = false;


    public ViewViewModel(IDialogService dialogService, ContractsService contractsService)
    {
        DialogService = dialogService;
        ContractsService = contractsService;
    }
    
    public async void CreateDialog()
    {
        var dialog = DialogService.Show<Create>("Adicionar Registo", new DialogOptions(){ CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Small});
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            await Table.ReloadServerData();
        }
    }
    
    public void OnSearch(string text)
    {
        SearchString = text;
        Table.ReloadServerData();
    }
    
    public async Task<PaginatedResponse<ProcessModel>?> GetDataAsync(TableState? state = null)
    {
        RequestOptions options = new RequestOptions()
        {
            FilterOptions = new()
            {
                Page = state.Page,
                PageSize = state.PageSize,
                SortDirection = state.SortDirection,
                SortLabel = state.SortLabel,
                Search = SearchString
            }
        };

        var paginatedResponse = await ContractsService.GetAsync(options);
        return paginatedResponse;
    }

    
    public async Task<TableData<ProcessModel>> ServerReload(TableState state)
    {
        var paginatedResponse = await GetDataAsync(state);
        return new TableData<ProcessModel>() {TotalItems = paginatedResponse.Total, Items = paginatedResponse.Documents};
    }
    
    public async Task RefreshTable()
    {
        if (!isBusy)
        {
            isBusy = true;
            await Table.ReloadServerData();
            isBusy = false;
        }
    }
}