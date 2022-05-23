using System.Net.Http.Json;
using System.Text;
using C21_PAP.Models;
using C21_PAP.Models.TiposPropriedades;
using C21_PAP.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MvvmBlazor.ViewModel;
using Newtonsoft.Json;

namespace C21_PAP.ViewModels.TiposPropriedades;

public class EditViewModel : ViewModelBase
{
    public MudDialogInstance MudDialog;
    
    readonly PropertyTypesService PropertyTypesService;
    readonly ISnackbar Snackbar;
    
    public MudForm form;
    public string[] errors = {};
    public bool success = false;

    public bool IsBusy = false;

    [Parameter] public TipoPropriedadeModel PropertyTypeModel { get; set; }

    public EditViewModel(PropertyTypesService propertyTypesService, ISnackbar snackbar)
    {
        PropertyTypesService = propertyTypesService;
        Snackbar = snackbar;
    }
    
    public async Task EditAsync()
    {
        IsBusy = true;
        await form.Validate();
        
        if (success)
        {
            try
            {
                var response = await PropertyTypesService.EditAsync(PropertyTypeModel);
                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add($"'{PropertyTypeModel.Name}' foi editado!", Severity.Success);
                }
                else
                {
                    Snackbar.Add($"Ocorreu um erro ao editar '{PropertyTypeModel.Name}'!", Severity.Error);

                }

            }
            catch
            {
                throw;
            }
            MudDialog.Close(DialogResult.Ok(PropertyTypeModel.Name));
        }
        IsBusy = false;
    }
}