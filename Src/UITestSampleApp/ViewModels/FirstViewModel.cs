using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

namespace UITestSampleApp
{
    public class FirstViewModel : BaseViewModel
    {
        #region Fields
        bool _isActivityIndicatorRunning;
        string _entryText, _labelText;
        Command<string> _goButtonCommand;
        #endregion

        #region Properties
        public Command<string> GoButtonCommand => 
            _goButtonCommand ?? (_goButtonCommand = new Command<string>(async goButtonText => await ExecuteGoButtonCommand(goButtonText)));

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
        #endregion

        #region Methods
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
        #endregion
    }
}
