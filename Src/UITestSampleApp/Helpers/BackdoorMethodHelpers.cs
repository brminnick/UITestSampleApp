#if DEBUG
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UITestSampleApp
{
    public static class BackdoorMethodHelpers
    {
        public static void BypassLoginScreen() => Application.Current.MainPage.Navigation.PopAsync();

        static Page GetCurrentPage()
        {
            if (Application.Current.MainPage.Navigation.ModalStack.Any())
                return Application.Current.MainPage.Navigation.ModalStack.LastOrDefault();

            return Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
        }
    }
}
#endif