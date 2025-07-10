using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CanvasReminders.ViewModels;

public partial class InputViewModel : ViewModelBase
{
    [ObservableProperty] 
    private string _inputText;

    public string link { get; set; }

    public bool GotLink { get; set; } = false;


    [RelayCommand]
    private void Submit()
    {
        GotLink = true;

        link = InputText;
    }
}