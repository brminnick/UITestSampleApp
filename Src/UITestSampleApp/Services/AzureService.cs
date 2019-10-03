using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AppCenter.Data;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
    public static class AppCenterDataService
    {
        static int _networkIndicatorCount;

        public static async IAsyncEnumerable<ListPageDataModel> GetListPageDataModels()
        {
            await UpdateNetworkActivityIndicatorStatus(true).ConfigureAwait(false);

            try
            {
                var result = await Data.ListAsync<ListPageDataModel>(DefaultPartitions.AppDocuments).ConfigureAwait(false);

                foreach (var listPageDataModel in GetListPageDataModel(result))
                    yield return listPageDataModel;

                while (result.HasNextPage)
                {
                    var nextPage = await result.GetNextPageAsync().ConfigureAwait(false);

                    foreach (var listPageDataModel in GetListPageDataModel(result))
                        yield return listPageDataModel;
                }

                static IEnumerable<ListPageDataModel> GetListPageDataModel(PaginatedDocuments<ListPageDataModel> documents) => documents.CurrentPage.Items.Select(x => x.DeserializedValue);
            }
            finally
            {
                await UpdateNetworkActivityIndicatorStatus(false).ConfigureAwait(false);
            }
        }

        public static async Task<ListPageDataModel> GetListPageDataModel(string documentId)
        {
            await UpdateNetworkActivityIndicatorStatus(true).ConfigureAwait(false);

            try
            {
                var item = await Data.ReadAsync<ListPageDataModel>(documentId, DefaultPartitions.AppDocuments).ConfigureAwait(false);
                return item.DeserializedValue;
            }
            finally
            {
                await UpdateNetworkActivityIndicatorStatus(false).ConfigureAwait(false);
            }
        }

        public static async Task<ListPageDataModel> UpdateListViewDataModel(ListPageDataModel item)
        {
            await UpdateNetworkActivityIndicatorStatus(true).ConfigureAwait(false);

            try
            {
                var response = await Data.ReplaceAsync(item.Id, item, DefaultPartitions.AppDocuments).ConfigureAwait(false);
                return response.DeserializedValue;
            }
            finally
            {
                await UpdateNetworkActivityIndicatorStatus(false).ConfigureAwait(false);
            }
        }

        public static async Task<ListPageDataModel> RemoveListPageDataModel(string documentId)
        {
            await UpdateNetworkActivityIndicatorStatus(true).ConfigureAwait(false);

            try
            {
                var response = await Data.DeleteAsync<ListPageDataModel>(documentId, DefaultPartitions.AppDocuments).ConfigureAwait(false);
                return response.DeserializedValue;
            }
            finally
            {
                await UpdateNetworkActivityIndicatorStatus(false).ConfigureAwait(false);
            }
        }

        static async Task UpdateNetworkActivityIndicatorStatus(bool shouldDisplayActivityIndicator)
        {
            if (shouldDisplayActivityIndicator)
            {
                _networkIndicatorCount++;
                await Xamarin.Forms.Device.InvokeOnMainThreadAsync(() => Xamarin.Forms.Application.Current.MainPage.IsBusy = true).ConfigureAwait(false);
            }
            else if (--_networkIndicatorCount <= 0)
            {
                _networkIndicatorCount = 0;
                await Xamarin.Forms.Device.InvokeOnMainThreadAsync(() => Xamarin.Forms.Application.Current.MainPage.IsBusy = false).ConfigureAwait(false);
            }
        }
    }
}
