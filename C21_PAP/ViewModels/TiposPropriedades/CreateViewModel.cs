using System.Net.Http.Json;
using C21_PAP.Models.TiposPropriedades;
using C21_PAP.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MvvmBlazor.ViewModel;


namespace C21_PAP.ViewModels.TiposPropriedades;

public class CreateViewModel : ViewModelBase
{
    readonly PropertyTypesService PropertyTypesService;
    readonly ISnackbar Snackbar;

    public MudDialogInstance MudDialog;
    public MudForm form;
    public string[] errors = {};
    public bool success = false;
    
    public bool IsBusy = false;
    
    public CreatePropertyTypeModel PropertyTypeModel = new();

    public CreateViewModel(PropertyTypesService propertyTypesService, ISnackbar snackbar)
    {
        PropertyTypesService = propertyTypesService;
        Snackbar = snackbar;
    }

    public async Task CreateAsync()
    {
        IsBusy = true;
        await form.Validate();

        if (success)
        {
            try
            {
                var res = await PropertyTypesService.CreateAsync(PropertyTypeModel);
                if (res.IsSuccessStatusCode)
                {
                    Snackbar.Add($"'{PropertyTypeModel.Name}' foi adicionado!", Severity.Success);
                }
                else
                {
                    Snackbar.Add($"Ocorreu um erro ao adicionar '{PropertyTypeModel.Name}'!", Severity.Error);
                }
            }
            catch
            {
                throw;
            }
            PropertyTypeModel = new();
            MudDialog.Close(DialogResult.Ok(PropertyTypeModel.Name));
            IsBusy = false;
        }
    }
}