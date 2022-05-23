using C21_PAP.Models.ContactOrigins;
using C21_PAP.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MvvmBlazor.ViewModel;

namespace C21_PAP.ViewModels.ContactOrigins;

public class EditViewModel : ViewModelBase
{
    public MudDialogInstance MudDialog;
    
    readonly ContactOriginsService ContactOriginsService;
    readonly ISnackbar Snackbar;
    
    public MudForm form;
    public string[] errors = {};
    public bool success = false;

    public bool IsBusy = false;

    [Parameter] public ContactOriginsModel ContactOriginsModel { get; set; }

    public EditViewModel(ContactOriginsService contactOriginsService, ISnackbar snackbar)
    {
        ContactOriginsService = contactOriginsService;
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
                var response = await ContactOriginsService.EditAsync(ContactOriginsModel);
                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add($"'{ContactOriginsModel.Name}' foi editado!", Severity.Success);
                }
                else
                {
                    Snackbar.Add($"Ocorreu um erro ao editar '{ContactOriginsModel.Name}'!", Severity.Error);

                }

            }
            catch
            {
                throw;
            }
            MudDialog.Close(DialogResult.Ok(ContactOriginsModel.Name));
        }
        IsBusy = false;
    }
}