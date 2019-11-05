using Xamarin.UITest;

using UITestSampleApp.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace UITestSampleApp.UITests
{
    class NewUserSignUpPage : BasePage
    {
        readonly Query _cancelButton, _passwordEntry, _saveUsernameButton, _usernameEntry;

        public NewUserSignUpPage(IApp app) : base(app, PageTitleConstants.NewUserSignUpPage)
        {
            _cancelButton = x => x.Marked(AutomationIdConstants.NewUserSignUpPage_CancelButton);
            _passwordEntry = x => x.Marked(AutomationIdConstants.NewUserSignUpPage_NewPasswordEntry);
            _saveUsernameButton = x => x.Marked(AutomationIdConstants.NewUserSignUpPage_SaveUsernameButton);
            _usernameEntry = x => x.Marked(AutomationIdConstants.NewUserSignUpPage_NewUserNameEntry);
        }

        public void CreateNewUserWithPassword(in string username, in string password, in bool shouldUseKeyboardReturnButton)
        {
            if (shouldUseKeyboardReturnButton)
                CreateNewUserWithPasswordUsingEnterButton(username, password);
            else
                CreateNewUserWithPasswordNotUsingEnterButton(username, password);
        }

        public void EnterUsername(in string username)
        {
            EnterText(_usernameEntry, username);
            App.Screenshot($"Entered Username: {username}");
        }

        public void EnterPassword(in string password)
        {
            EnterText(_passwordEntry, password);
            App.Screenshot($"Entered Password: {password}");
        }

        public void TapSave()
        {
            App.Tap(_saveUsernameButton);
            App.Screenshot("Tapped Save Button");
        }

        public void TapCancel()
        {
            App.Tap(_cancelButton);
            App.Screenshot("Tapped Cancel Button");
        }

        void CreateNewUserWithPasswordNotUsingEnterButton(in string username, in string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            TapSave();
        }

        void CreateNewUserWithPasswordUsingEnterButton(in string username, in string password)
        {
            EnterText(_usernameEntry, username, false);
            App.Screenshot($"Entered Username: {username}");

            App.PressEnter();

            App.EnterText(password);
            App.Screenshot($"Entered Password: {password}");

            App.PressEnter();

            App.Screenshot("New User Created Using Enter Button");
        }
    }
}