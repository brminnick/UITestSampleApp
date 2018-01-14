using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using MyLoginUI.Pages;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
    public class LoginPage : ReusableLoginPage
    {
        #region Fields
        bool isInitialized = false;
        #endregion

        #region Constructors
        public LoginPage() : base("xamarin_logo")
        {
            AutomationId = "loginPage";

#if DEBUG
            var crashButton = new Button
            {
                Text = "x",
                TextColor = Color.White,
                BackgroundColor = Color.Transparent,
                AutomationId = AutomationIdConstants.CrashButton
            };
            crashButton.Clicked += (s, e) => throw new Exception("Crash Button Tapped");

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
            var success = await DependencyService.Get<ILogin>().CheckLogin(username, password);
            if (success)
                await Navigation.PopAsync();
            else
            {
                var isSignupSelected = await DisplayAlert("Invalid Login", "Sorry, we didn't recoginize the username or password. Feel free to sign up for free if you haven't!", "Sign up", "Try again");

                if (isSignupSelected)
                {
                    await Navigation.PushModalAsync(new NewUserSignUpPage());

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

            if (isInitialized)
                return;

            Navigation.InsertPageBefore(new FirstPage(), this);
            isInitialized = true;
        }
        #endregion
    }
}