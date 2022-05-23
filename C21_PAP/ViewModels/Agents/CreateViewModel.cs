using C21_PAP.Models.Agents;
using C21_PAP.Services;
using MudBlazor;
using MvvmBlazor.ViewModel;

namespace C21_PAP.ViewModels.Agents;

public class CreateViewModel : ViewModelBase
{
    readonly AgentService AgentService;
    readonly ISnackbar Snackbar;

    public MudDialogInstance MudDialog;
    public MudForm form;
    public string[] errors = {};
    public bool success = false;

    public List<string> AgentTypes { get; set; } = new();
    
    public bool IsBusy = false;
    
    public CreateAgentModel CreateAgentModel = new();
    
    public CreateViewModel(AgentService agentService, ISnackbar snackbar)
    {
        AgentService = agentService;
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
                var res = await AgentService.CreateAsync(CreateAgentModel);
                if (res.IsSuccessStatusCode)
                {
                    Snackbar.Add($"'{CreateAgentModel.Name}' foi adicionado!", Severity.Success);
                }
                else
                {
                    Snackbar.Add($"Ocorreu um erro ao adicionar '{CreateAgentModel.Name}'!", Severity.Error);
                }
            }
            catch
            {
                throw;
            }
            MudDialog.Close(DialogResult.Ok(CreateAgentModel.Name));
            CreateAgentModel = new();
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