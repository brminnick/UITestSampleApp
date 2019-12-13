using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using AsyncAwaitBestPractices.MVVM;

using UITestSampleApp.Shared;

using Xamarin.Essentials;

namespace UITestSampleApp
{
    public class ListViewModel : BaseViewModel
    {
        bool _isRefreshing;
        ICommand? _pullToRefreshCommand;
        IReadOnlyList<ListPageDataModel> _dataList = Enumerable.Empty<ListPageDataModel>().ToList();

        public ICommand PullToRefreshCommand => _pullToRefreshCommand ??= new AsyncCommand(ExecutePullToRefreshCommanded);

        public IReadOnlyList<ListPageDataModel> DataList
        {
            get => _dataList;
            set => SetProperty(ref _dataList, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        async Task RefreshDataFromAzureAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                return;

            var dataList = new List<ListPageDataModel>();

            try
            {
                await foreach (var listPageDataModel in AppCenterDataService.GetListPageDataModels().ConfigureAwait(false))
                {
                    dataList.Add(listPageDataModel);
                }

                DataList = dataList;
            }
            catch (Exception e)
            {
                AppCenterHelpers.Report(e);
            }
        }

        async Task ExecutePullToRefreshCommanded()
        {
            try
            {
                AppCenterHelpers.TrackEvent(AppCenterConstants.PullToRefreshCommanded);

                var showRefreshIndicatorForOneSecondTask = Task.Delay(1000);

                await Task.WhenAll(RefreshDataFromAzureAsync(), showRefreshIndicatorForOneSecondTask).ConfigureAwait(false);
            }
            finally
            {
                IsRefreshing = false;
            }
        }
    }
}
