using System;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;

using AsyncAwaitBestPractices.MVVM;

using Xamarin.Essentials;

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
            (_pullToRefreshCommand = new AsyncCommand(ExecutePullToRefreshCommanded, continueOnCapturedContext: false));

        public List<ListPageDataModel> DataList
        {
            get => _dataList;
            set
            {
                value = value.OrderBy(x => x.Detail).ToList();
                SetProperty(ref _dataList, value);
            }
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
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                return;

            try
            {
                await AzureService.Instance.SyncItemsAsync<ListPageDataModel>().ConfigureAwait(false);

                var dataList = await AzureService.Instance.GetItemsAsync<ListPageDataModel>().ConfigureAwait(false);
                DataList = dataList.ToList();
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
                var dataList = await AzureService.Instance.GetItemsAsync<ListPageDataModel>().ConfigureAwait(false);
                DataList = dataList?.ToList();
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
