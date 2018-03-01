using System;

using Xamarin.UITest;

using UITestSampleApp.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace UITestSampleApp.UITests
{
    class LoginPage : BasePage
    {
        readonly Query _forgotPasswordButton;
        readonly Query _loginButton;
        readonly Query _passwordEntry;
        readonly Query _signUpButton;
        readonly Query _usernameEntry;
        readonly Query _crashButton;
        readonly Query _crashButtonDialogYesButton;
        readonly Query _crashButtonDialogNoButton;

        public LoginPage(IApp app) : base(app, PageTitleConstants.LoginPage)
        {
            _forgotPasswordButton = x => x.Marked(AutomationIdConstants.LoginPage_ForgotPasswordButton);
            _loginButton = x => x.Marked(AutomationIdConstants.LoginPage_LoginButton);
            _passwordEntry = x => x.Marked(AutomationIdConstants.LoginPage_PasswordEntry);
            _signUpButton = x => x.Marked(AutomationIdConstants.LoginPage_NewUserSignUpButton);
            _usernameEntry = x => x.Marked(AutomationIdConstants.LoginPage_UsernameEntry);
            _crashButton = x => x.Marked(AutomationIdConstants.LoginPage_CrashButton);
            _crashButtonDialogYesButton = x => x.Marked(CrashDialogConstants.Yes);
            _crashButtonDialogNoButton = x => x.Marked(CrashDialogConstants.No);
        }

        public void LoginWithUsernamePassword(string username, string password, bool shouldUseKeyboardReturnButton)
        {
            switch (shouldUseKeyboardReturnButton)
            {
                case true:
                    LoginWithUsernamePasswordUsingEnterButton(username, password);
                    break;
                case false:
                    LoginWithUsernamePasswordNotUsingEnterButton(username, password);
                    break;
            }
        }

        public void EnterUsername(string username)
        {
            UITestHelpers.EnterText(_usernameEntry, username, App);
            App.Screenshot($"Entered Username: {username}");
        }

        public void EnterPassword(string password)
        {
            UITestHelpers.EnterText(_passwordEntry, password, App);
            App.Screenshot($"Entered Password: {password}");
        }

        public void PressSignUpButton()
        {
            App.Tap(_signUpButton);
            App.Screenshot("Tapped Sign Up Button");
        }

        public void PressForgotPasswordButton()
        {
            App.Tap(_forgotPasswordButton);
            App.Screenshot("Tapped Forgot Password Button");
        }

        public void PressLoginButton()
        {
            App.Tap(_loginButton);
            App.Screenshot("Tapped Login Button");
        }

        public void SignUpNewUserFromDialog()
        {
            LoginWithUsernamePasswordNotUsingEnterButton("incorrectUserName", "incorrectPassword");
            TapSignUpFromDialog();
        }

        public void TapSignUpFromDialog(int timeoutInSeconds = 60)
        {
            App.WaitForElement("Sign up", "Sign Up Dialog Did Not Appear", TimeSpan.FromSeconds(timeoutInSeconds));
            App.Tap("Sign up");
            App.Screenshot("Tapped Sign Up Button From Pop Up Dialog");
        }

        public void TapTryAgainDialog()
        {
            App.Tap("Try again");
            App.Screenshot("Tapped Try Again Button From Pop Up Dialog");
        }

        public void TapOkayOnErrorDialog()
        {
            App.Tap("Okay");
            App.Screenshot("Tapped Okay on Error Dialog");
        }

        public override void WaitForPageToLoad()
        {
            App.WaitForElement(_loginButton, "Login Screen Did Not Appear");
        }

        public void TapCrashButton()
        {
            App.Tap(_crashButton);
            App.Screenshot("Crash Button Tapped");
        }

        public void TapYes_CrashButtonDialog()
        {
            App.Tap(_crashButtonDialogYesButton);
            App.Screenshot("Yes Tapped on Crash Button Dialog");
        }

        public void TapNo_CrashButtonDialog()
        {
            App.Tap(_crashButtonDialogNoButton);
            App.Screenshot("No Tapped on Crash Button Dialog");
        }

        void LoginWithUsernamePasswordNotUsingEnterButton(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            PressLoginButton();
        }

        void LoginWithUsernamePasswordUsingEnterButton(string username, string password)
        {
            App.Tap(_usernameEntry);
            App.ClearText();
            App.EnterText(username);
            App.Screenshot($"Entered Username: {username}");

            App.PressEnter();


            App.ClearText();
            App.EnterText(password);
            App.Screenshot($"Entered Password: {password}");

            App.PressEnter();

            App.Screenshot("Logged In Using Enter Button");
        }
    }
}