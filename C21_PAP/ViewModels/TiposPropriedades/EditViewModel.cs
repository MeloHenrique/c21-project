using System.Net.Http.Json;
using System.Text;
using C21_PAP.Models;
using C21_PAP.Models.TiposPropriedades;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MvvmBlazor.ViewModel;
using Newtonsoft.Json;

namespace C21_PAP.ViewModels.TiposPropriedades;

public class EditViewModel : ViewModelBase
{
    public MudDialogInstance MudDialog;
    
    readonly HttpClient HttpClient;
    readonly ISnackbar Snackbar;
    
    public MudForm form;
    public string[] errors = {};
    public bool success = false;

    public bool IsBusy = false;

    [Parameter] public TipoPropriedadeModel PropertyTypeModel { get; set; }

    public EditViewModel(HttpClient client, ISnackbar snackbar)
    {
        HttpClient = client;
        Snackbar = snackbar;
    }
    
    public async Task EditAsync()
    {
        IsBusy = true;
        await form.Validate();
        
        if (success)
        {
            var request = await HttpClient.PostAsJsonAsync("/edit", PropertyTypeModel);
            if (request.IsSuccessStatusCode)
            {
                Snackbar.Add($"'{PropertyTypeModel.Name}' foi editado!", Severity.Success);
            }
            else
            {
                Snackbar.Add($"Ocorreu um erro ao editar '{PropertyTypeModel.Name}'!", Severity.Error);

            }
            MudDialog.Close(DialogResult.Ok(PropertyTypeModel.Name));
            IsBusy = false;
        }
    }
}