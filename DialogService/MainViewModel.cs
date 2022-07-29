using System.Windows.Input;
using DevExpress.Mvvm;

namespace DialogService
{
    public class MainViewModel 
    {
        private readonly IDialogService _dialogService;
        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public string Message { get; set; } = "Hello World!";

        public ICommand ShowDialogCommand => new CommandHandler(() => _dialogService.ShowDialog(MessageButton.YesNo, "Test Dialog", "Test Message" ), true);
    }
}