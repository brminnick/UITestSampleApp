using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;

using AsyncAwaitBestPractices.MVVM;

namespace UITestSampleApp
{
    public class FirstViewModel : BaseViewModel
    {
        bool _isActivityIndicatorRunning;
        string _entryText = string.Empty;
        string _labelText = string.Empty;
        ICommand? _goButtonCommand;

        public ICommand GoButtonCommand => _goButtonCommand ??= new AsyncCommand<string>(ExecuteGoButtonCommand);

        public bool IsActiityIndicatorRunning
        {
            get => _isActivityIndicatorRunning;
            set => SetProperty(ref _isActivityIndicatorRunning, value);
        }

        public string EntryText
        {
            get => _entryText;
            set => SetProperty(ref _entryText, value);
        }

        public string LabelText
        {
            get => _labelText;
            set => SetProperty(ref _labelText, value);
        }

        async Task ExecuteGoButtonCommand(string goButtonText)
        {
            AppCenterHelpers.TrackEvent(AppCenterConstants.GoButtonTapped, new Dictionary<string, string> {
                { AppCenterConstants.FirstPageTextEntered, goButtonText }
            });

            LabelText = string.Empty;
            IsActiityIndicatorRunning = true;

            await Task.Delay(1500).ConfigureAwait(false);

            IsActiityIndicatorRunning = false;
            LabelText = goButtonText;
        }
    }
}
