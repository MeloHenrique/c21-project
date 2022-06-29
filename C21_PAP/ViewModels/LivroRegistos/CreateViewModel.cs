using C21_PAP.Models.PostalCodeInfo;
using C21_PAP.Models.Process;
using C21_PAP.Services;
using MudBlazor;
using MvvmBlazor.ViewModel;

namespace C21_PAP.ViewModels.LivroRegistos;

public class CreateViewModel : ViewModelBase
{
    readonly ISnackbar Snackbar;    
    readonly ContractsService ContractsService;

    
    public MudDialogInstance MudDialog;

    public bool IsBusy = false;
    
    public List<MudForm> Forms = new()
    {
        new(),
        new()
    };
    public int step = 0;
    
    public PostalCodeInfo postalCodeInfo { get; set; } = new()
    {
        Counties = new(),
        Districts = new(),
        Parishes = new(),
        Streets = new()
    };
    
    public CreateProcessModel CreateProcessModel = new()
    {
        Property = new()
    };
    
    public CreateViewModel(ContractsService contractsService, ISnackbar snackbar)
    {
        ContractsService = contractsService;
        Snackbar = snackbar;
    }
    
    public async Task PostalCodeChanged(string postalCode)
    {
        
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
                    PostalCode = CreateProcessModel.Property.PostalCode,
                    Street = postalCodeInfo.Streets.First()
                };
            }
            catch
            {
                throw new ApplicationException("Error converting the postal code!");
            }
        }
    }
    
    public Task<bool> NextStep()
    {
        Forms[step].Validate();
        if (Forms[step].IsValid)
        {
            step++;
        }

        return new Task<bool>(() => true);
    }

    public Task<bool> BackStep()
    {
        step--;
        return new Task<bool>(() => true);
    }

}