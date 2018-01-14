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
        bool _isRefreshing;
        ICommand _pullToRefreshCommand;
        List<ListPageDataModel> _dataList;
        #endregion

        #region Properties
        public ICommand PullToRefreshCommand => _pullToRefreshCommand ??
            (_pullToRefreshCommand = new Command(async () => await ExecutePullToRefreshCommanded()));

        public List<ListPageDataModel> DataList
        {
            get => _dataList;
            set => SetProperty(ref _dataList, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }
        #endregion

        #region Methods
        async Task RefreshDataFromAzureAsync()
        {
            var isAzureDatabaseReachable = await CrossConnectivity.Current.IsRemoteReachable(AzureConstants.AzureDataServiceUrl, 80, 1000).ConfigureAwait(false);
            if (!CrossConnectivity.Current.IsConnected || !isAzureDatabaseReachable)
                return;

            try
            {
                await DependencyService.Get<IDataService>().SyncItemsAsync<ListPageDataModel>().ConfigureAwait(false);

                var dataListAsIEnumerable = await DependencyService.Get<IDataService>().GetItemsAsync<ListPageDataModel>().ConfigureAwait(false);
                DataList = dataListAsIEnumerable.ToList();
            }
            catch (Exception e)
            {
                AppCenterHelpers.LogException(e);
            }
        }

        async Task RefreshDataFromLocalDatabaseAsync()
        {
            try
            {
                var dataListAsIEnumerable = await DependencyService.Get<IDataService>().GetItemsAsync<ListPageDataModel>().ConfigureAwait(false);
                DataList = dataListAsIEnumerable?.ToList();
            }
            catch (Exception e)
            {
                AppCenterHelpers.LogException(e);
            }
        }

        async Task ExecutePullToRefreshCommanded()
        {
            IsRefreshing = true;

            try
            {
                AppCenterHelpers.TrackEvent(AppCenterConstants.PullToRefreshCommanded);

                var showRefreshIndicatorForOneSecondTask = Task.Delay(1000);

                await Task.WhenAll(RefreshDataAsync(), showRefreshIndicatorForOneSecondTask).ConfigureAwait(false);
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        async Task RefreshDataAsync()
        {
            await RefreshDataFromLocalDatabaseAsync().ConfigureAwait(false);
            await RefreshDataFromAzureAsync().ConfigureAwait(false);
        }
        #endregion
    }
}
