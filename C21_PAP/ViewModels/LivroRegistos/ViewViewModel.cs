using C21_PAP.Pages.LivroRegistos;
using C21_PAP.Services;
using MudBlazor;
using MvvmBlazor.ViewModel;

namespace C21_PAP.ViewModels.LivroRegistos;

public class ViewViewModel : ViewModelBase
{
    readonly IDialogService DialogService;

    public ViewViewModel(IDialogService dialogService)
    {
        DialogService = dialogService;
    }
    
    public async void CreateDialog()
    {
        var dialog = DialogService.Show<Create>("Adicionar Registo", new DialogOptions(){ CloseButton = true,});
        var result = await dialog.Result;
        if (!result.Cancelled)
        {
          //  await CategoryTypes.Table.ReloadServerData();
        }
    }
}