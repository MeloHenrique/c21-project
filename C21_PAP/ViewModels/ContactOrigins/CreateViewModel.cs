using C21_PAP.Models.ContactOrigins;
using C21_PAP.Services;
using MudBlazor;

namespace C21_PAP.ViewModels.ContactOrigins;

public class CreateViewModel
{
    readonly ContactOriginsService ContactOriginsService;
    readonly ISnackbar Snackbar;

    public MudDialogInstance MudDialog;
    public MudForm form;
    public string[] errors = {};
    public bool success = false;
    
    public bool IsBusy = false;
    
    public CreateContactOriginModel CreateContactOrigin = new();
    
    public CreateViewModel(ContactOriginsService contactOriginsService, ISnackbar snackbar)
    {
        ContactOriginsService = contactOriginsService;
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
                var res = await ContactOriginsService.CreateAsync(CreateContactOrigin);
                if (res.IsSuccessStatusCode)
                {
                    Snackbar.Add($"'{CreateContactOrigin.Name}' foi adicionado!", Severity.Success);
                }
                else
                {
                    Snackbar.Add($"Ocorreu um erro ao adicionar '{CreateContactOrigin.Name}'!", Severity.Error);
                }
            }
            catch
            {
                throw;
            }
            MudDialog.Close(DialogResult.Ok(CreateContactOrigin.Name));
            CreateContactOrigin = new();
            IsBusy = false;
        }
    }
}