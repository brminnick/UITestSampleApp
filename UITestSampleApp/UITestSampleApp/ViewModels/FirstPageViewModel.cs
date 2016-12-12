using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using UITestSampleApp.Shared;
using Xamarin.Forms;
namespace UITestSampleApp
{
	public class FirstPageViewModel : BaseViewModel
	{
		#region Fields
		bool _isActivityIndicatorRunning;
		string _entryText, _labelText;
		ICommand _goButtonTapped;
		#endregion

		#region Properties
		public bool IsActiityIndicatorRunning
		{
			get { return _isActivityIndicatorRunning; }
			set { SetProperty(ref _isActivityIndicatorRunning, value); }
		}

		public string EntryText
		{
			get { return _entryText; }
			set { SetProperty(ref _entryText, value); }
		}

		public string LabelText
		{
			get { return _labelText; }
			set { SetProperty(ref _labelText, value); }
		}

		public ICommand GoButtonTapped =>
		_goButtonTapped ??
		(_goButtonTapped = new Command(async () => await ExecuteGoButtonTapped()));
		#endregion

		#region Methods
		async Task ExecuteGoButtonTapped()
		{
			AnalyticsHelpers.TrackEvent(AnalyticsConstants.GoButtonTapped, new Dictionary<string, string> {
				{ AnalyticsConstants.FirstPageTextEntered, EntryText }
			});

			IsActiityIndicatorRunning = true;

			await Task.Delay(2000);

			IsActiityIndicatorRunning = false;
			LabelText = EntryText;
		}

		#endregion
	}
}
