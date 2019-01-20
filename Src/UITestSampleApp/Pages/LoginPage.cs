using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using MyLoginUI.Pages;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
    public class LoginPage : ReusableLoginPage
    {
        #region Constructors
        public LoginPage() : base("xamarin_logo")
        {
#if DEBUG
            var crashButton = new Button
            {
                Text = "x",
                TextColor = Color.White,
                BackgroundColor = Color.Transparent,
                AutomationId = AutomationIdConstants.LoginPage_CrashButton
            };
            crashButton.Clicked += HandleCrashButtonClicked;

            MainLayout.Children.Add(crashButton,
                Constraint.RelativeToParent(parent => parent.X),
                Constraint.RelativeToParent(parent => parent.Y)
            );
#endif
        }
        #endregion

        #region Methods
        protected override async Task Login(string username, string password)
        {
            var isLoginValid = await SecureStorageService.IsLoginCorrect(username, password).ConfigureAwait(false);
            if (isLoginValid)
                Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
            else
            {
                var isSignupSelected = await DisplayAlert("Invalid Login", "Sorry, we didn't recoginize the username or password. Feel free to sign up for free if you haven't!", "Sign up", "Try again");

                if (isSignupSelected)
                {
                    Device.BeginInvokeOnMainThread(async () => await Navigation.PushModalAsync(new NewUserSignUpPage()));

                    AppCenterHelpers.TrackEvent("NewUserSignUp", new Dictionary<string, string> {
                        { "ActionPoint", "System Prompt" },
                    });
                }
            }
        }

        protected override async Task NewUserSignUp() => await Navigation.PushModalAsync(new NewUserSignUpPage());

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!Navigation.NavigationStack.OfType<FirstPage>().Any())
                Navigation.InsertPageBefore(new FirstPage(), this);
        }

#if DEBUG
        async void HandleCrashButtonClicked(object sender, EventArgs e)
        {
            try
            {
                AppCenterHelpers.CrashApp();
            }
            catch (Exception ex)
            {
                AppCenterHelpers.LogException(ex);

                var isCrashConfirmed = await DisplayAlert(CrashDialogConstants.Title, CrashDialogConstants.Message, CrashDialogConstants.Yes, CrashDialogConstants.No);

                if (isCrashConfirmed)
                    throw;
            }
        }
#endif
        #endregion
    }
}