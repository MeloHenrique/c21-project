using C21_PAP.Models;
using C21_PAP.Models.Agents;
using C21_PAP.Pages.Agents;
using C21_PAP.Services;
using MudBlazor;
using MvvmBlazor.ViewModel;

namespace C21_PAP.ViewModels.Agents;

public class ViewViewModel : ViewModelBase
{
    public MudTable<AgentModel> Table;
    readonly AgentService AgentService;
    readonly IDialogService DialogService;
    
    private string? SearchString = null;
    
    public ViewViewModel(AgentService agentService, IDialogService dialogService)
    {
        AgentService = agentService;
        DialogService = dialogService;
    }
    
    public async Task<TableData<AgentModel>> ServerReload(TableState state)
    {
        var paginatedResponse = await GetDataAsync(state);
        return new TableData<AgentModel>() {TotalItems = paginatedResponse.Total, Items = paginatedResponse.Documents};
    }

    public async Task<PaginatedResponse<AgentModel>?> GetDataAsync(TableState? state = null)
    {
        TableRequestOptions options = new TableRequestOptions()
        {
            Page = state.Page,
            PageSize = state.PageSize,
            SortDirection = state.SortDirection,
            SortLabel = state.SortLabel,
            Search = SearchString
        };

        var paginatedResponse = await AgentService.GetAsync(options);
        return paginatedResponse;
    }
    
    public void OnSearch(string text)
    {
        SearchString = text;
        Table.ReloadServerData();
    }
    
    public async void CreateDialog()
    {
        var dialog = DialogService.Show<Create>("Adicionar Agente", new DialogOptions(){ CloseButton = true, FullWidth = true});
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            await Table.ReloadServerData();
        }
    }
}