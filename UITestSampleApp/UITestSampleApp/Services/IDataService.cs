using System.Collections.Generic;
using System.Threading.Tasks;

namespace UITestSampleApp
{
	public interface IDataService
	{
		Task<IEnumerable<T>> GetItemsAsync<T>() where T : EntityData;
		Task<IEnumerable<T>> GetItemsFromLocalDatabaseAsync<T>() where T : EntityData;
		Task AddItemAsync<T>(T item) where T : EntityData;
		Task UpdateItemAsync<T>(T item) where T : EntityData;
		Task RemoveItemAsync<T>(T item) where T : EntityData;
	}
}
