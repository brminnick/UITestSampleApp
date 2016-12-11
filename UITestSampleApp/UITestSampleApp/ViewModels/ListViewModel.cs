using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UITestSampleApp
{
	public class ListViewModel : BaseViewModel
	{
		#region Fields
		List<ListViewData> _dataList;
		#endregion

		#region Constructors
		public ListViewModel()
		{
			//DataList = SampleDataModelFactory.GetSampleData().ToList();
			DataList = Task.Run(async () => await DependencyService.Get<IDataService>().GetItems<ListViewData>()).Result.ToList();
		}
		#endregion

		#region Properties
		public List<ListViewData> DataList
		{
			get { return _dataList; }
			set { SetProperty(ref _dataList, value); }
		}
		#endregion
	}
}
