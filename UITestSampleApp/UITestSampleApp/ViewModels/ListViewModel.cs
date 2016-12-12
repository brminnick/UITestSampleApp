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

		List<ListPageDataModel> _dataList;
		#endregion

		#region Constructors
		public ListViewModel()
		{
			Task.Run(async () => await RefreshDataFromAzureAsync());
		}
		#endregion

		#region Events
		public event EventHandler LoadingDataFromBackendCompleted;
		#endregion

		#region Properties
		public List<ListPageDataModel> DataList
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
		(_pullToRefreshCommanded = new Command(async () => await ExecutePullToRefreshCommanded()));
		#endregion

		#region Methods
		async Task RefreshDataFromAzureAsync()
		{
			IsDataLoadingFromBackend = true;

			try
			{
				await Task.Run(async () => DataList = (await DependencyService.Get<IDataService>().GetItems<ListPageDataModel>()).ToList());
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

		async Task ExecutePullToRefreshCommanded()
		{
			AnalyticsHelpers.TrackEvent(AnalyticsConstants.PullToRefreshCommanded);

			await RefreshDataFromAzureAsync();

			OnLoadingDataFromBackendCompleted();
		}
		#endregion
	}
}
