using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

using Xamarin.Forms;

namespace UITestSampleApp
{
	public class AzureService : IDataService
	{
		#region Constant Fields
		const string _azureDataServiceUrl = @"https://mobile-864df958-bcca-401d-8f93-ae159cd5a9d3.azurewebsites.net";
		#endregion 

		#region Fields
		bool _isInitialized;
		#endregion

		#region Properties
		public MobileServiceClient MobileService { get; set; }
		#endregion

		#region Methods
		public async Task Initialize()
		{
			if (_isInitialized)
				return;

			// MobileServiceClient handles communication with our backend, auth, and more for us.
			MobileService = new MobileServiceClient(_azureDataServiceUrl);

			// Configure online/offline sync.
			var path = DependencyService.Get<IEnvironment>().GetFilePath("app.db");
			var store = new MobileServiceSQLiteStore(path);
			store.DefineTable<ListPageDataModel>();
			await MobileService.SyncContext.InitializeAsync(store, new SyncHandler(MobileService));

			_isInitialized = true;
		}

		public async Task<IEnumerable<T>> GetItems<T>() where T : EntityData
		{
			await Initialize();

			await SyncItems<T>();

			return await MobileService.GetSyncTable<T>().ToEnumerableAsync();
		}

		public async Task<T> GetItem<T>(string id) where T : EntityData
		{
			await Initialize();

			await SyncItems<T>();

			return await MobileService.GetSyncTable<T>().LookupAsync(id);
		}

		public async Task AddItem<T>(T item) where T : EntityData
		{
			await Initialize();

			await MobileService.GetSyncTable<T>().InsertAsync(item);
			await SyncItems<T>();
		}

		public async Task UpdateItem<T>(T item) where T : EntityData
		{
			await Initialize();

			await MobileService.GetSyncTable<T>().UpdateAsync(item);
			await SyncItems<T>();
		}

		public async Task RemoveItem<T>(T item) where T : EntityData
		{
			await Initialize();

			await MobileService.GetSyncTable<T>().DeleteAsync(item);
			await SyncItems<T>();
		}

		public async Task SyncItems<T>() where T : EntityData
		{
			await Initialize();

			try
			{
				await MobileService.SyncContext.PushAsync();
				await MobileService.GetSyncTable<T>().PullAsync($"all{typeof(T).Name}", MobileService.GetSyncTable<T>().CreateQuery());
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Error during Sync occurred: {ex.Message}");
			}
		}
		#endregion
	}
}
