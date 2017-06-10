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
        ICommand _goButtonCommand;
        #endregion

        #region Properties
        public ICommand GoButtonCommand => 
            _goButtonCommand ?? (_goButtonCommand = new Command(async () => await ExecuteGoButtonCommand()));

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
        async Task ExecuteGoButtonCommand()
        {
            MobileCenterHelpers.TrackEvent(MobileCenterConstants.GoButtonTapped, new Dictionary<string, string> {
                { MobileCenterConstants.FirstPageTextEntered, EntryText }
            });

            IsActiityIndicatorRunning = true;

            await Task.Delay(1500);

            IsActiityIndicatorRunning = false;
            LabelText = EntryText;
        }

        #endregion
    }
}
