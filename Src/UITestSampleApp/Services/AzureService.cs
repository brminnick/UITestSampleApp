using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AppCenter.Data;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
    public static class AzureService
    {
        #region Fields
        static int _networkIndicatorCount;
        #endregion

        #region Methods
        public static async Task<List<ListPageDataModel>> GetListPageDataModels()
        {
            UpdateNetworkActivityIndicatorStatus(true);

            try
            {
                var itemList = new List<ListPageDataModel>();

                var result = await Data.ListAsync<ListPageDataModel>(DefaultPartitions.AppDocuments).ConfigureAwait(false);
                itemList.AddRange(result.CurrentPage.Items.Select(x => x.DeserializedValue));

                while (result.HasNextPage)
                {
                    var nextPage = await result.GetNextPageAsync().ConfigureAwait(false);
                    itemList.AddRange(nextPage.Items.Select(x => x.DeserializedValue));
                }

                return itemList;
            }
            finally
            {
                UpdateNetworkActivityIndicatorStatus(false);
            }
        }

        public static async Task<ListPageDataModel> GetListPageDataModel(string documentId)
        {
            UpdateNetworkActivityIndicatorStatus(true);

            try
            {
                var item = await Data.ReadAsync<ListPageDataModel>(documentId, DefaultPartitions.AppDocuments).ConfigureAwait(false);
                return item.DeserializedValue;
            }
            finally
            {
                UpdateNetworkActivityIndicatorStatus(false);
            }
        }

        public static async Task<ListPageDataModel> UpdateListViewDataModel(ListPageDataModel item)
        {
            UpdateNetworkActivityIndicatorStatus(true);

            try
            {
                var response = await Data.ReplaceAsync(item.Id, item, DefaultPartitions.AppDocuments).ConfigureAwait(false);
                return response.DeserializedValue;
            }
            finally
            {
                UpdateNetworkActivityIndicatorStatus(false);
            }
        }

        public static async Task<ListPageDataModel> RemoveListPageDataModel(string documentId)
        {
            UpdateNetworkActivityIndicatorStatus(true);

            try
            {
                var response = await Data.DeleteAsync<ListPageDataModel>(documentId, DefaultPartitions.AppDocuments).ConfigureAwait(false);
                return response.DeserializedValue;
            }
            finally
            {
                UpdateNetworkActivityIndicatorStatus(false);
            }
        }

        static void UpdateNetworkActivityIndicatorStatus(bool isActivityIndicatorDisplayed)
        {
            if (isActivityIndicatorDisplayed)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => Xamarin.Forms.Application.Current.MainPage.IsBusy = true);
                _networkIndicatorCount++;
            }
            else if (--_networkIndicatorCount <= 0)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => Xamarin.Forms.Application.Current.MainPage.IsBusy = false);
                _networkIndicatorCount = 0;
            }
        }
        #endregion
    }
}
