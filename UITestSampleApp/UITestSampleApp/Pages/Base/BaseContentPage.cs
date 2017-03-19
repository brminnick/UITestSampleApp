using Xamarin.Forms;

namespace UITestSampleApp
{
	public abstract class BaseContentPage<T> : ContentPage where T : BaseViewModel, new()
	{
		T _viewModel;

		protected BaseContentPage()
		{
			BackgroundColor = Color.FromHex("#2980b9");
			BindingContext = ViewModel;
		}

		protected T ViewModel => _viewModel ?? (_viewModel = new T());
	}
}

