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
		#endregion

		#region Fields
		MobileServiceClient _mobileService;
		#endregion

		#region Methods
		public async Task<IEnumerable<T>> GetItemsAsync<T>() where T : EntityData
		{
			await Initialize<T>();

			return await _mobileService.GetSyncTable<T>().ToEnumerableAsync();
		}

		public async Task<T> GetItem<T>(string id) where T : EntityData
		{
			await Initialize<T>();

			return await _mobileService.GetSyncTable<T>().LookupAsync(id);
		}

		public async Task AddItemAsync<T>(T item) where T : EntityData
		{
			await Initialize<T>();

			await _mobileService.GetSyncTable<T>().InsertAsync(item);
		}

		public async Task UpdateItemAsync<T>(T item) where T : EntityData
		{
			await Initialize<T>();

			await _mobileService.GetSyncTable<T>().UpdateAsync(item);
		}

		public async Task RemoveItemAsync<T>(T item) where T : EntityData
		{
			await Initialize<T>();

			await _mobileService.GetSyncTable<T>().DeleteAsync(item);
		}

		public async Task SyncItemsAsync<T>() where T : EntityData
		{
			await Initialize<T>();

			try
			{
				await _mobileService.GetSyncTable<T>().PullAsync($"all{typeof(T).Name}", _mobileService.GetSyncTable<T>().CreateQuery());
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Error during Sync occurred: {ex.Message}");
			}
		}

		async Task Initialize<T>() where T : EntityData
		{
			if (IsDataTypeInitialized<T>())
				return;
			
			if (_mobileService == null)
				_mobileService = new MobileServiceClient(AzureConstants.AzureDataServiceUrl);
			
			_isInitializedDictionary?.Add(typeof(T), false);

			await ConfigureOnlineOfflineSync<T>();

			_isInitializedDictionary[typeof(T)] = true;
		}

		async Task ConfigureOnlineOfflineSync<T>() where T : EntityData
		{
			var path = Path.Combine(MobileServiceClient.DefaultDatabasePath, "app.db");
			var store = new MobileServiceSQLiteStore(path);
			store.DefineTable<T>();

			await _mobileService.SyncContext.InitializeAsync(store, new SyncHandler());
		}

		bool IsDataTypeInitialized<T>() where T : EntityData
		{
			var isDataTypeInitalized = _isInitializedDictionary?.FirstOrDefault(x => x.Key.Equals(typeof(T))).Value;
			return isDataTypeInitalized == true;
		}
		#endregion
	}
}
