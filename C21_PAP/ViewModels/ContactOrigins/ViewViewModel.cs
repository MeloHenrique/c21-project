using C21_PAP.Models;
using C21_PAP.Models.ContactOrigins;
using C21_PAP.Pages.ContactOrigins;
using C21_PAP.Services;
using MudBlazor;
using MvvmBlazor.ViewModel;

namespace C21_PAP.ViewModels.ContactOrigins;

public class ViewViewModel : ViewModelBase
{
    public MudTable<ContactOriginsModel> Table;
    readonly ContactOriginsService ContactOriginsService;
    readonly IDialogService DialogService;
    
    private string? SearchString = null;
    
    public bool isBusy = false;
    
    public ViewViewModel(ContactOriginsService contactOriginsService, IDialogService dialogService)
    {
        ContactOriginsService = contactOriginsService;
        DialogService = dialogService;
    }
    
    public async Task<TableData<ContactOriginsModel>> ServerReload(TableState state)
    {
        var paginatedResponse = await GetDataAsync(state);
        return new TableData<ContactOriginsModel>() {TotalItems = paginatedResponse.Total, Items = paginatedResponse.Documents};
    }

    public async Task<PaginatedResponse<ContactOriginsModel>?> GetDataAsync(TableState? state = null)
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

        var paginatedResponse = await ContactOriginsService.GetAsync(options);
        return paginatedResponse;
    }
    
    public void OnSearch(string text)
    {
        SearchString = text;
        Table.ReloadServerData();
    }
    
    public async void CreateDialog()
    {
        var dialog = DialogService.Show<Create>("Adicionar Origem de Contato", new DialogOptions(){ CloseButton = true, FullWidth = true});
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            await Table.ReloadServerData();
        }
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
    
    public async void EditDialog(ContactOriginsModel model)
    {
        var parameters = new DialogParameters(){ ["ContactOriginsModel"]=model };;
        var dialog = DialogService.Show<Edit>("Editar Origem de Contato", parameters, new DialogOptions(){ CloseButton = true, FullWidth = true});
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            await Table.ReloadServerData();
        }
    }
}