using System;
using System.Collections.ObjectModel;
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

        public ICommand PullToRefreshCommand => _pullToRefreshCommand ??= new AsyncCommand(ExecutePullToRefreshCommanded);

        public ObservableCollection<ListPageDataModel> DataList { get; } = new ObservableCollection<ListPageDataModel>();

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        async Task RefreshDataFromAzureAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                return;

            DataList.Clear();

            try
            {
                await foreach (var listPageDataModel in AppCenterDataService.GetListPageDataModels().ConfigureAwait(false))
                {
                    DataList.Add(listPageDataModel);
                }
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
