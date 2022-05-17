using System.Net.Http.Json;
using C21_PAP.Models.TiposPropriedades;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MvvmBlazor.ViewModel;


namespace C21_PAP.ViewModels.TiposPropriedades;

public class CreateViewModel : ViewModelBase
{
    readonly HttpClient HttpClient;
    readonly ISnackbar Snackbar;

    public MudDialogInstance MudDialog;
    public MudForm form;
    public string[] errors = {};
    public bool success = false;
    
    public CreatePropertyTypeModel PropertyTypeModel = new();

    public CreateViewModel(HttpClient client, ISnackbar snackbar)
    {
        HttpClient = client;
        Snackbar = snackbar;
    }

    public async Task CreateAsync()
    {
        await form.Validate();

        if (success)
        {
            var res = await HttpClient.PostAsJsonAsync("/create/", PropertyTypeModel);
            if (res.IsSuccessStatusCode)
            {
                Snackbar.Add($"'{PropertyTypeModel.Name}' foi adicionado!", Severity.Success);
            }
            else
            {
                Snackbar.Add($"Ocorreu um erro ao adicionar '{PropertyTypeModel.Name}'!", Severity.Error);
            }
            PropertyTypeModel = new();
            MudDialog.Close(DialogResult.Ok(PropertyTypeModel.Name));
        }
    }
}