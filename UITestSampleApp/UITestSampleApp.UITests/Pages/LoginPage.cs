using System;

using Xamarin.UITest;

using UITestSampleApp.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace UITestSampleApp.UITests
{
	public class LoginPage : BasePage
	{
		readonly Query _forgotPasswordButton;
		readonly Query _loginButton;
		readonly Query _passwordEntry;
		readonly Query _signUpButton;
		readonly Query _usernameEntry;
		readonly Query _crashButton;

		public LoginPage(IApp app, Platform platform)
			: base(app, platform)
		{
            if(OnAndroid)
            {
                _forgotPasswordButton = x => x.Class("AppCompatButton").Index(2);
            }
            else
            {
                _forgotPasswordButton = x => x.Class("UIButton").Index(2);
            }

			_forgotPasswordButton = x => x.Marked("ForgotPasswordButton");
			_loginButton = x => x.Marked(AutomationIdConstants.LoginButton);
			_passwordEntry = x => x.Marked(AutomationIdConstants.PasswordEntry);
			_signUpButton = x => x.Marked(AutomationIdConstants.NewUserButton);
			_usernameEntry = x => x.Marked(AutomationIdConstants.UsernameEntry);
			_crashButton = x => x.Marked(AutomationIdConstants.CrashButton);
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

		public void WaitForLoginScreen(int timeoutInSeconds = 60)
		{
			App.WaitForElement(_loginButton, "Login Screen Did Not Appear", TimeSpan.FromSeconds(timeoutInSeconds));
		}

		public void TapCrashButton()
		{
			App.Tap(_crashButton);
			App.Screenshot("Crash Button Tapped");
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