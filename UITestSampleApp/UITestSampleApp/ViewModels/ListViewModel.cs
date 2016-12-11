using System.Collections.Generic;
using System.Linq;

namespace UITestSampleApp
{
	public class ListViewModel : BaseViewModel
	{
		#region Fields
		List<SampleDataModel> _dataList;
		#endregion

		#region Constructors
		public ListViewModel()
		{
			DataList = SampleDataModelFactory.GetSampleData().ToList();
			//var listViewData = Task.Run(async () => await DependencyService.Get<IDataService>().GetItems<ListViewData>()).Result;
		}
		#endregion

		#region Properties
		public List<SampleDataModel> DataList
		{
			get { return _dataList; }
			set { SetProperty(ref _dataList, value); }
		}
		#endregion
	}
}
