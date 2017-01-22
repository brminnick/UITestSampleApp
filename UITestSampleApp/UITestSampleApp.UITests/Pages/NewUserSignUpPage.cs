using Xamarin.UITest;

using UITestSampleApp.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace UITestSampleApp.UITests
{
	public class NewUserSignUpPage : BasePage
	{
		readonly Query _cancelButton;
		readonly Query _passwordEntry;
		readonly Query _saveUsernameButton;
		readonly Query _usernameEntry;

		public NewUserSignUpPage(IApp app, Platform platform)
			: base(app, platform)
		{
			_cancelButton = x => x.Marked(AutomationIdConstants.CancelButton);
			_passwordEntry = x => x.Marked(AutomationIdConstants.NewPasswordEntry);
			_saveUsernameButton = x => x.Marked(AutomationIdConstants.SaveUsernameButton);
			_usernameEntry = x => x.Marked(AutomationIdConstants.NewUserNameEntry);
		}

		public void CreateNewUserWithPassword(string username, string password, bool shouldUseKeyboardReturnButton)
		{
			switch(shouldUseKeyboardReturnButton)
			{
				case true:
					CreateNewUserWithPasswordUsingEnterButton(username, password);
					break;
				case false:
					CreateNewUserWithPasswordNotUsingEnterButton(username, password);
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

		public void TapSave()
		{
			app.Tap(_saveUsernameButton);
			app.Screenshot("Tapped Save Button");
		}

		public void TapCancel()
		{
			app.Tap(_cancelButton);
			app.Screenshot("Tapped Cancel Button");
		}

		void CreateNewUserWithPasswordNotUsingEnterButton(string username, string password)
		{
			EnterUsername(username);
			EnterPassword(password);
			TapSave();
		}

		void CreateNewUserWithPasswordUsingEnterButton(string username, string password)
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

			app.Screenshot("New User Created Using Enter Button");
		}
	}
}