using C21_PAP.Models.Agents;
using C21_PAP.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MvvmBlazor.ViewModel;

namespace C21_PAP.ViewModels.Agents;

public class EditViewModel : ViewModelBase
{
    public MudDialogInstance MudDialog;
    
    readonly AgentService AgentService;
    readonly ISnackbar Snackbar;
    
    public MudForm form;
    public string[] errors = {};
    public bool success = false;

    public bool IsBusy = false;
    
    public List<string> AgentTypes { get; set; } = new();

    [Parameter] public AgentModel AgentModel { get; set; }
    
    public EditViewModel(AgentService agentService, ISnackbar snackbar)
    {
        AgentService = agentService;
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
                var response = await AgentService.EditAsync(AgentModel);
                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add($"'{AgentModel.Name}' foi editado!", Severity.Success);
                }
                else
                {
                    Snackbar.Add($"Ocorreu um erro ao editar '{AgentModel.Name}'!", Severity.Error);

                }

            }
            catch
            {
                throw;
            }
            MudDialog.Close(DialogResult.Ok(AgentModel.Name));
        }
        IsBusy = false;
    }
    
    public async Task getAgentTypes()
    {
        try
        {
            var res = await AgentService.GetAgentTypeAsync();
            var resL = res.Documents.ToList();
            AgentTypes = resL;
            StateHasChanged();
        }
        catch
        {
            throw;
        }
    }
}