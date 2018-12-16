using Xamarin.Forms;

namespace UITestSampleApp
{
    public abstract class BaseContentPage<T> : ContentPage where T : BaseViewModel, new()
    {
        #region Constructors
        protected BaseContentPage(string pageTitle)
        {
            BindingContext = ViewModel;
            BackgroundColor = Color.FromHex("#2980b9");
            Title = pageTitle;
        }
        #endregion

        #region Properties
        protected T ViewModel { get; } = new T();
        #endregion
    }
}

