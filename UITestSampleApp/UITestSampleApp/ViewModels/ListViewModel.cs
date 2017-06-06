using System;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using Plugin.Connectivity;

namespace UITestSampleApp
{
	public class ListViewModel : BaseViewModel
	{
		#region Fields
		bool _isDataLoadingFromBackend;
        ICommand _pullToRefreshCommand;
		List<ListPageDataModel> _dataList;
		#endregion

		#region Events
		public event EventHandler LoadingDataFromBackendCompleted;
		#endregion

		#region Properties
		public ICommand PullToRefreshCommand => _pullToRefreshCommand ??
			(_pullToRefreshCommand = new Command(async () => await ExecutePullToRefreshCommanded()));

		public List<ListPageDataModel> DataList
		{
			get => _dataList;
			set => SetProperty(ref _dataList, value);
		}

		public bool IsDataLoading
		{
			get => _isDataLoadingFromBackend;
			set => SetProperty(ref _isDataLoadingFromBackend, value);
		}
		#endregion

		#region Methods
		async Task RefreshDataFromAzureAsync()
		{
			var isAzureDatabaseReachable = await CrossConnectivity.Current.IsRemoteReachable(AzureConstants.AzureDataServiceUrl, 80, 1000);
			if (!CrossConnectivity.Current.IsConnected || !isAzureDatabaseReachable)
				return;

			try
			{
				await DependencyService.Get<IDataService>().SyncItemsAsync<ListPageDataModel>();
				var dataListAsIEnumerable = await DependencyService.Get<IDataService>().GetItemsAsync<ListPageDataModel>();
				DataList = dataListAsIEnumerable.ToList();
			}
			catch (Exception e)
			{
				MobileCenterHelpers.Log("Error Retrieving Data From Azure", e.Message, e);
			}
		}

		async Task RefreshDataFromLocalDatabaseAsync()
		{
			try
			{
				var dataListAsIEnumerable = await DependencyService.Get<IDataService>().GetItemsAsync<ListPageDataModel>();
				DataList = dataListAsIEnumerable?.ToList();
			}
			catch (Exception e)
			{
				MobileCenterHelpers.Log("Error Retriving Data From Local Database", e.Message, e);
			}
		}

		async Task ExecutePullToRefreshCommanded()
		{
			MobileCenterHelpers.TrackEvent(MobileCenterConstants.PullToRefreshCommanded);

			await RefreshDataAsync();

			OnLoadingDataFromBackendCompleted();
		}

		async Task RefreshDataAsync()
		{
			IsDataLoading = true;

			await RefreshDataFromLocalDatabaseAsync();
			await RefreshDataFromAzureAsync();

			OnLoadingDataFromBackendCompleted();

			IsDataLoading = false;
		}

		void OnLoadingDataFromBackendCompleted() =>
			LoadingDataFromBackendCompleted?.Invoke(null, EventArgs.Empty);
		#endregion
	}
}
