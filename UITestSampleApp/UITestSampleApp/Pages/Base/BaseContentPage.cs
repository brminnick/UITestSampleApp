using Xamarin.Forms;

namespace UITestSampleApp
{
	public abstract class BaseContentPage<T> : ContentPage where T : BaseViewModel, new()
	{
		protected BaseContentPage()
		{
			BackgroundColor = Color.FromHex("#2980b9");
            ViewModel = new T();
            BindingContext = ViewModel;
		}

        protected T ViewModel { get; }
	}
}

