using System.Net.Http.Json;
using MvvmBlazor.ViewModel;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using C21_PAP.Models;
using C21_PAP.Models.TiposPropriedades;
using C21_PAP.Pages.TiposPropriedade;
using C21_PAP.Services;
using MudBlazor;

namespace C21_PAP.ViewModels.TiposPropriedades;

public class ViewViewModel : ViewModelBase
{
    public MudTable<TipoPropriedadeModel> table;
    readonly PropertyTypesService PropertyTypesService;
    readonly IDialogService DialogService;
    
    private string? SearchString = null;


    
    public ViewViewModel(PropertyTypesService propertyTypesService, IDialogService dialogService)
    {
        PropertyTypesService = propertyTypesService;
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
        var paginatedResponse = await GetDataAsync(state);
        return new TableData<TipoPropriedadeModel>() {TotalItems = paginatedResponse.Total, Items = paginatedResponse.Documents};
    }

    public async Task<PaginatedResponse<TipoPropriedadeModel>?> GetDataAsync(TableState? state = null)
    {
        TableRequestOptions options = new TableRequestOptions()
        {
            Page = state.Page,
            PageSize = state.PageSize,
            SortDirection = state.SortDirection,
            SortLabel = state.SortLabel,
            Search = SearchString
        };

        var paginatedResponse = await PropertyTypesService.GetAsync(options);
        return paginatedResponse;
    }
    
    public void OnSearch(string text)
    {
        SearchString = text;
        table.ReloadServerData();
    }
}