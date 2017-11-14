using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
    public class AzureService : IDataService
    {
        #region Constant Fields
        readonly Dictionary<Type, bool> _isInitializedDictionary = new Dictionary<Type, bool>();
        readonly Lazy<MobileServiceClient> _mobileServiceClientHolder = new Lazy<MobileServiceClient>(() => new MobileServiceClient(AzureConstants.AzureDataServiceUrl));
        #endregion

        #region Fields
        int _networkIndicatorCount;
        #endregion

        #region Properties
        MobileServiceClient MobileServiceClient => _mobileServiceClientHolder.Value;
        #endregion

        #region Methods
        public async Task<IEnumerable<T>> GetItemsAsync<T>() where T : EntityData
        {
            await Initialize<T>();

            return await MobileServiceClient.GetSyncTable<T>().ToEnumerableAsync();
        }

        public async Task<T> GetItem<T>(string id) where T : EntityData
        {
            await Initialize<T>();

            return await MobileServiceClient.GetSyncTable<T>().LookupAsync(id);
        }

        public async Task AddItemAsync<T>(T item) where T : EntityData
        {
            await Initialize<T>();

            await MobileServiceClient.GetSyncTable<T>().InsertAsync(item);
        }

        public async Task UpdateItemAsync<T>(T item) where T : EntityData
        {
            await Initialize<T>();

            await MobileServiceClient.GetSyncTable<T>().UpdateAsync(item);
        }

        public async Task RemoveItemAsync<T>(T item) where T : EntityData
        {
            await Initialize<T>();

            await MobileServiceClient.GetSyncTable<T>().DeleteAsync(item);
        }

        public async Task SyncItemsAsync<T>() where T : EntityData
        {
            await Initialize<T>();

            UpdateNetworkActivityIndicatorStatus(true);

            try
            {
                await MobileServiceClient.GetSyncTable<T>().PullAsync($"all{typeof(T).Name}", MobileServiceClient.GetSyncTable<T>().CreateQuery());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error during Sync occurred: {ex.Message}");
            }
            finally
            {
                UpdateNetworkActivityIndicatorStatus(false);
            }
        }

        async Task Initialize<T>() where T : EntityData
        {
            if (IsDataTypeInitialized<T>())
                return;

            _isInitializedDictionary?.Add(typeof(T), false);

            await ConfigureOnlineOfflineSync<T>();

            _isInitializedDictionary[typeof(T)] = true;
        }

        async Task ConfigureOnlineOfflineSync<T>() where T : EntityData
        {
            var path = Path.Combine(MobileServiceClient.DefaultDatabasePath, "app.db");
            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<T>();

            await MobileServiceClient.SyncContext.InitializeAsync(store, new SyncHandler());
        }

        bool IsDataTypeInitialized<T>() where T : EntityData
        {
            var isDataTypeInitalized = _isInitializedDictionary?.FirstOrDefault(x => x.Key.Equals(typeof(T))).Value;
            return isDataTypeInitalized == true;
        }

        void UpdateNetworkActivityIndicatorStatus(bool isActivityIndicatorDisplayed)
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
