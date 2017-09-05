using System.Threading.Tasks;
using System.Collections.Generic;

namespace UITestSampleApp.Shared
{
	public interface IDataService
	{
		Task<IEnumerable<T>> GetItemsAsync<T>() where T : EntityData;
		Task<T> GetItem<T>(string id) where T : EntityData;
		Task AddItemAsync<T>(T item) where T : EntityData;
		Task UpdateItemAsync<T>(T item) where T : EntityData;
		Task RemoveItemAsync<T>(T item) where T : EntityData;
		Task SyncItemsAsync<T>() where T : EntityData;
	}
}
