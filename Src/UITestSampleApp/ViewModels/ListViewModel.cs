using System;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using Plugin.Connectivity;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
    public class ListViewModel : BaseViewModel
    {
        #region Fields
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
                MobileCenterHelpers.Log(e);
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
                MobileCenterHelpers.Log(e);
            }
        }

        async Task ExecutePullToRefreshCommanded()
        {
            MobileCenterHelpers.TrackEvent(MobileCenterConstants.PullToRefreshCommanded);

            var showRefreshIndicatorForOneSecondTask = Task.Delay(1000);

            await Task.WhenAll(RefreshDataAsync(), showRefreshIndicatorForOneSecondTask);

            OnLoadingDataFromBackendCompleted();
        }

        async Task RefreshDataAsync()
        {
            IsAccessingInternet = true;

            await RefreshDataFromLocalDatabaseAsync();
            await RefreshDataFromAzureAsync();

            OnLoadingDataFromBackendCompleted();

            IsAccessingInternet = false;
        }

        void OnLoadingDataFromBackendCompleted() =>
            LoadingDataFromBackendCompleted?.Invoke(null, EventArgs.Empty);
        #endregion
    }
}
