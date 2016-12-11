using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Windows.Input;
using UITestSampleApp.Shared;

namespace UITestSampleApp
{
	public class ListViewModel : BaseViewModel
	{
		#region Fields
		bool _isDataLoadingFromBackend;

		ICommand _pullToRefreshCommanded;

		List<ListViewPageData> _dataList;
		#endregion

		#region Constructors
		public ListViewModel()
		{
			RefreshDataFromAzure();
		}
		#endregion

		#region Events
		public event EventHandler LoadingDataFromBackendCompleted;
		#endregion

		#region Properties
		public List<ListViewPageData> DataList
		{
			get { return _dataList; }
			set { SetProperty(ref _dataList, value); }
		}

		public bool IsDataLoadingFromBackend
		{
			get { return _isDataLoadingFromBackend; }
			set { SetProperty(ref _isDataLoadingFromBackend, value); }
		}

		public ICommand PullToRefreshCommanded =>
		_pullToRefreshCommanded ??
		(_pullToRefreshCommanded = new Command(ExecutePullToRefreshCommanded));
		#endregion

		#region Methods
		async Task RefreshDataFromAzure()
		{
			IsDataLoadingFromBackend = true;

			try
			{
				DataList = (await DependencyService.Get<IDataService>().GetItems<ListViewPageData>()).ToList();
			}
			catch (Exception e)
			{
				AnalyticsHelpers.Log("Error Retrieving Data From Azure", e.Message, e);
			}
			finally
			{
				OnLoadingDataFromBackendCompleted();
				IsDataLoadingFromBackend = false;
			}

		}

		void OnLoadingDataFromBackendCompleted()
		{
			var handle = LoadingDataFromBackendCompleted;
			handle?.Invoke(null, EventArgs.Empty);
		}

		async void ExecutePullToRefreshCommanded()
		{
			AnalyticsHelpers.TrackEvent(AnalyticsConstants.PullToRefreshCommanded);

			await RefreshDataFromAzure();

		}
		#endregion
	}
}
