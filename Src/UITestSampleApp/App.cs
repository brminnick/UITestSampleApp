using Xamarin.Forms;

namespace UITestSampleApp
{
    public class App : Application
    {
        public App()
        {
            var page = new LoginPage();
            NavigationPage.SetHasNavigationBar(page, false);

            MainPage = new NavigationPage(page)
            {
                BarBackgroundColor = Color.FromHex("#3498db"),
                BarTextColor = Color.White,
            };
        }

        protected override void OnStart()
        {
            base.OnStart();

            AppCenterHelpers.Start();
        }
    }
}


