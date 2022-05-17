using System.Net.Http.Json;
using MvvmBlazor.ViewModel;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using C21_PAP.Models;
using C21_PAP.Models.TiposPropriedades;
using C21_PAP.Pages.TiposPropriedade;
using MudBlazor;

namespace C21_PAP.ViewModels.TiposPropriedades;

public class ViewViewModel : ViewModelBase
{
    public MudTable<TipoPropriedadeModel> table;
    readonly HttpClient HttpClient;
    readonly IDialogService DialogService;

    
    public ViewViewModel(HttpClient client, IDialogService dialogService)
    {
        HttpClient = client;
        DialogService = dialogService;
    }

    public async void CreateDialog()
    {
        var dialog = DialogService.Show<Create>("Adicionar Tipo Propriedade", new DialogOptions(){ CloseButton = true, FullWidth = true});
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            await table.ReloadServerData();
        }
    }

    public async void EditDialog(TipoPropriedadeModel model)
    {
        var parameters = new DialogParameters(){ ["PropertyTypeModel"]=model };;
        var dialog = DialogService.Show<Edit>("Editar Tipo Propriedade", parameters, new DialogOptions(){ CloseButton = true, FullWidth = true});
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
            await table.ReloadServerData();
        }
    }
    
    public async Task<TableData<TipoPropriedadeModel>> ServerReload(TableState state)
    {
        
        var elements = await GetDataAsync(state);
        return new TableData<TipoPropriedadeModel>() {TotalItems = elements.Count(), Items = elements};
    }

    public async Task<IEnumerable<TipoPropriedadeModel>?> GetDataAsync(TableState? state = null)
    {
        try
        {
            var res = await HttpClient.PostAsJsonAsync("/get-all/", state);
            var elements = await res.Content.ReadFromJsonAsync<Response<TipoPropriedadeModel>>(); 
            return elements?.Documents;
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
            return null;
        }
    }
}