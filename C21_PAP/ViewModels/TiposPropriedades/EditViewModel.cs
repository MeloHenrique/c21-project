using System.Text;
using C21_PAP.Models.TiposPropriedades;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MvvmBlazor.ViewModel;
using Newtonsoft.Json;

namespace C21_PAP.ViewModels.TiposPropriedades;

public class EditViewModel : ViewModelBase
{
    readonly HttpClient HttpClient;
    readonly ISnackbar Snackbar;
    
    public MudForm form;
    public string[] errors = {};
    public bool success = false;

    [Parameter] public TipoPropriedadeModel PropertyTypeModel { get; set; }

    public EditViewModel(HttpClient client, ISnackbar snackbar)
    {
        HttpClient = client;
        Snackbar = snackbar;
    }
    
    public async Task EditAsync()
    {
        await form.Validate();
        HttpContent header = new StringContent(JsonConvert.SerializeObject(PropertyTypeModel), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(new HttpMethod("PATCH"), "/api/tipos-propriedade/") {Content = header};
        if (success)
        {
            var res = await HttpClient.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
                Snackbar.Add($"'{PropertyTypeModel.Name}' foi editado!", Severity.Success);
            }
            else
            {
                Snackbar.Add($"Ocorreu um erro ao adicionar '{PropertyTypeModel.Name}'!", Severity.Error);

            }
        }
    }
}