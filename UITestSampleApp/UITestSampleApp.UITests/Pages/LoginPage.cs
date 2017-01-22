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
		readonly Query _rememberMeSwitch;
		readonly Query _signUpButton;
		readonly Query _usernameEntry;
		readonly Query _crashButton;

		public LoginPage(IApp app, Platform platform)
			: base(app, platform)
		{
			_forgotPasswordButton = x => x.Marked(AutomationIdConstants.ForgotPasswordButton);
			_loginButton = x => x.Marked(AutomationIdConstants.LoginButton);
			_passwordEntry = x => x.Marked(AutomationIdConstants.PasswordEntry);
			_rememberMeSwitch = x => x.Marked(AutomationIdConstants.SaveUsernameSwitch);
			_signUpButton = x => x.Marked(AutomationIdConstants.NewUserButton);
			_usernameEntry = x => x.Marked(AutomationIdConstants.UsernameEntry);
			_crashButton = x => x.Marked(AutomationIdConstants.CrashButton);
		}

		public void LoginWithUsernamePassword(string username, string password, bool shouldUseKeyboardReturnButton)
		{
			switch(shouldUseKeyboardReturnButton)
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
			app.Tap(_usernameEntry);
			app.ClearText();
			app.EnterText(username);
			app.DismissKeyboard();
			app.Screenshot($"Entered Username: {username}");
		}

		public void EnterPassword(string password)
		{
			app.Tap(_passwordEntry);
			app.ClearText();
			app.EnterText(password);
			app.DismissKeyboard();
			app.Screenshot($"Entered Password: {password}");
		}

		public void PressSignUpButton()
		{
			app.Tap(_signUpButton);
			app.Screenshot("Tapped Sign Up Button");
		}

		public void PressForgotPasswordButton()
		{
			app.Tap(_forgotPasswordButton);
			app.Screenshot("Tapped Forgot Password Button");
		}

		public void PressLoginButton()
		{
			app.Tap(_loginButton);
			app.Screenshot("Tapped Login Button");
		}

		public void SignUpNewUserFromDialog()
		{
			LoginWithUsernamePasswordNotUsingEnterButton("incorrectUserName", "incorrectPassword");
			TapSignUpFromDialog();
		}

		public void TapSignUpFromDialog(int timeoutInSeconds = 60)
		{
			app.WaitForElement("Sign up", "Sign Up Dialog Did Not Appear", TimeSpan.FromSeconds(timeoutInSeconds));
			app.Tap("Sign up");
			app.Screenshot("Tapped Sign Up Button From Pop Up Dialog");
		}

		public void TapTryAgainDialog()
		{
			app.Tap("Try again");
			app.Screenshot("Tapped Try Again Button From Pop Up Dialog");
		}

		public void TapOkayOnErrorDialog()
		{
			app.Tap("Okay");
			app.Screenshot("Tapped Okay on Error Dialog");
		}

		public void WaitForLoginScreen(int timeoutInSeconds = 60)
		{
			app.WaitForElement(_loginButton, "Login Screen Did Not Appear", TimeSpan.FromSeconds(timeoutInSeconds));
		}

		public void TapCrashButton()
		{
			app.Tap(_crashButton);
			app.Screenshot("Crash Button Tapped");
		}

		void LoginWithUsernamePasswordNotUsingEnterButton(string username, string password)
		{
			EnterUsername(username);
			EnterPassword(password);
			PressLoginButton();
		}

		void LoginWithUsernamePasswordUsingEnterButton(string username, string password)
		{
			app.Tap(_usernameEntry);
			app.ClearText();
			app.EnterText(username);
			app.Screenshot($"Entered Username: {username}");

			app.PressEnter();


			app.ClearText();
			app.EnterText(password);
			app.Screenshot($"Entered Password: {password}");

			app.PressEnter();

			app.Screenshot("Logged In Using Enter Button");
		}
	}
}