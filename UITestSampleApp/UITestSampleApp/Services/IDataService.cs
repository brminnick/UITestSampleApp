using System.Collections.Generic;
using System.Threading.Tasks;

namespace UITestSampleApp
{
	public interface IDataService
	{
		Task Initialize();
		Task<IEnumerable<T>> GetItems<T>() where T : EntityData;
		Task AddItem<T>(T item) where T : EntityData;
		Task UpdateItem<T>(T item) where T : EntityData;
		Task RemoveItem<T>(T item) where T : EntityData;
		Task SyncItems<T>() where T : EntityData;
	}
}
