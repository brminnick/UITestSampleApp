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
        #region Constant Fields
#if DEBUG
        readonly Button _crashButton;
#endif
        #endregion

        #region Fields
        bool isInitialized = false;
        #endregion

        #region Constructors
        public LoginPage() : base("xamarin_logo")
        {
            AutomationId = "loginPage";

#if DEBUG
            _crashButton = new Button
            {
                Text = "x",
                TextColor = Color.White,
                BackgroundColor = Color.Transparent,
                AutomationId = AutomationIdConstants.LoginPage_CrashButton
            };

            MainLayout.Children.Add(_crashButton,
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

#if DEBUG
            _crashButton.Clicked += HandleCrashButtonClicked;
#endif

            if (isInitialized)
                return;

            Navigation.InsertPageBefore(new FirstPage(), this);
            isInitialized = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

#if DEBUG
            _crashButton.Clicked -= HandleCrashButtonClicked;
#endif
        }


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
        #endregion
    }
}