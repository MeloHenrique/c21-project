using C21_PAP.Models;
using C21_PAP.Models.Agents;
using C21_PAP.Models.Contract;
using C21_PAP.Models.PostalCodeInfo;
using C21_PAP.Models.Process;
using C21_PAP.Models.TiposPropriedades;
using C21_PAP.Services;
using MudBlazor;
using MvvmBlazor.ViewModel;

namespace C21_PAP.ViewModels.LivroRegistos;

public class CreateViewModel : ViewModelBase
{
    readonly ISnackbar Snackbar;    
    readonly ContractsService ContractsService;
    public List<string> TargetBusiness = new(){"Venda", "Arrendamento"};
    public List<string> Regime = new(){"Exclusivo", "Aberto"};
    public List<string> Status = new(){"Ativo"};
    public MudDialogInstance MudDialog;
    public List<TipoPropriedadeModel> PropertyTypes = new();
    public List<AgentModel> Agents = new();

    public List<bool> Success { get; set; } = new() {
        false,
        false
    };
    public List<string[]> Errors { get; set; } = new() {
        new string[] {},
        new string[] {},
    };

    public bool IsBusy;
    
    public List<MudForm> Forms = new() {
        new(),
        new()
    };

    public PostalCodeInfo postalCodeInfo { get; set; } = new() {
        Counties = new(),
        Districts = new(),
        Parishes = new(),
        Streets = new()
    };
    
    public CreateProcessModel CreateProcessModel = new() {
        Property = new(),
        Contract = new()
        {
            Months = 6
        },
    };
    
    public CreateViewModel(ContractsService contractsService, ISnackbar snackbar)
    {
        ContractsService = contractsService;
        Snackbar = snackbar;
    }
    
    public async Task PostalCodeChanged(string postalCode)
    {
        CreateProcessModel.Property.PostalCode = postalCode;
        if (postalCode.Length == 8)
        {
            try
            {
                var res = await ContractsService.GetPostalCodeInfo(Int32.Parse(postalCode.Replace("-", "")));
                postalCodeInfo = res;
                CreateProcessModel.Property = new()
                {
                    District = res.Districts.First(),
                    County = res.Counties.First(),
                    Parish = res.Parishes.First(),
                    PostalCode = postalCode,
                    Street = postalCodeInfo.Streets.First(),
                    PropertyTypeId = CreateProcessModel.Property.PropertyTypeId
                };
            }
            catch
            {
                throw new ApplicationException("Error converting the postal code!");
            }
        }
    }

    public void AgentChanged(string agentId)
    {
        CreateProcessModel.Contract.RaisingAgent = agentId;
        if(Agents.Where(x => x._Id == agentId).First().Type == "Agente Externo")
        {
            CreateProcessModel.Contract.ContractType = ContractType.External.ToString();
        }
        else
        {
            CreateProcessModel.Contract.ContractType = ContractType.Internal.ToString();
        }
    }
    
    public async Task NextStep(bool finalized)
    {
        IsBusy = true;
        if (finalized)
        {
            var res = await ContractsService.Create(CreateProcessModel);
            if (res.IsSuccessStatusCode)
            {
                Snackbar.Add("O processo foi adicionado!", Severity.Success);
            }
            else
            {
                Snackbar.Add("Ocorreu um erro ao adicionar o processo!", Severity.Error);
            }
            MudDialog.Close(DialogResult.Ok(CreateProcessModel));
            CreateProcessModel = new()
            {
                Contract = new()
                {
                    Months = 6
                },
                Property = new()
            };
            
        }
        IsBusy = false;
    }

    public async Task<bool> NextValidation(int step)
    {
        IsBusy = true;
        await Forms[step].Validate();
        IsBusy = false;
        return Success[step];
    }
}