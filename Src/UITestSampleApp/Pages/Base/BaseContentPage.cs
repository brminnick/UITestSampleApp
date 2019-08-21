using Xamarin.Forms;

namespace UITestSampleApp
{
    public abstract class BaseContentPage<T> : ContentPage where T : BaseViewModel, new()
    {
        protected BaseContentPage(string pageTitle)
        {
            BindingContext = ViewModel;
            BackgroundColor = Color.FromHex("#2980b9");
            Title = pageTitle;
        }

        protected T ViewModel { get; } = new T();
    }
}

