using Xamarin.Forms;

namespace UITestSampleApp
{
    public abstract class BaseContentPage<T> : ContentPage where T : BaseViewModel, new()
    {
        #region Fields
        T _viewModel;
        #endregion

        #region Constructors
        protected BaseContentPage(string pageTitle)
        {
            BindingContext = ViewModel;
            BackgroundColor = Color.FromHex("#2980b9");
            Title = pageTitle;
        }
        #endregion

        #region Properties
        protected T ViewModel => _viewModel ?? (_viewModel = new T());
        #endregion

        #region Methods
        protected override void OnAppearing()
        {
            base.OnAppearing();

            SubscribeEventHandlers();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            UnsubscribeEventHandlers();
        }

        protected abstract void SubscribeEventHandlers();
        protected abstract void UnsubscribeEventHandlers();
        #endregion
    }
}

