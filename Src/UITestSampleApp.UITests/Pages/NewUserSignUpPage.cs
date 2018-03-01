using Xamarin.UITest;

using UITestSampleApp.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace UITestSampleApp.UITests
{
    class NewUserSignUpPage : BasePage
    {
        readonly Query _cancelButton;
        readonly Query _passwordEntry;
        readonly Query _saveUsernameButton;
        readonly Query _usernameEntry;

        public NewUserSignUpPage(IApp app) : base(app, PageTitleConstants.NewUserSignUpPage)
        {
            _cancelButton = x => x.Marked(AutomationIdConstants.NewUserSignUpPage_CancelButton);
            _passwordEntry = x => x.Marked(AutomationIdConstants.NewUserSignUpPage_NewPasswordEntry);
            _saveUsernameButton = x => x.Marked(AutomationIdConstants.NewUserSignUpPage_SaveUsernameButton);
            _usernameEntry = x => x.Marked(AutomationIdConstants.NewUserSignUpPage_NewUserNameEntry);
        }

        public void CreateNewUserWithPassword(string username, string password, bool shouldUseKeyboardReturnButton)
        {
            switch (shouldUseKeyboardReturnButton)
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
            App.Tap(_usernameEntry);
            App.ClearText();
            App.EnterText(username);
            App.DismissKeyboard();
            App.Screenshot($"Entered Username: {username}");
        }

        public void EnterPassword(string password)
        {
            App.Tap(_passwordEntry);
            App.ClearText();
            App.EnterText(password);
            App.DismissKeyboard();
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

        void CreateNewUserWithPasswordNotUsingEnterButton(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            TapSave();
        }

        void CreateNewUserWithPasswordUsingEnterButton(string username, string password)
        {
            App.ClearText(_usernameEntry);
            App.EnterText(_usernameEntry, username);
            App.Screenshot($"Entered Username: {username}");

            App.PressEnter();


            App.ClearText();
            App.EnterText(password);
            App.Screenshot($"Entered Password: {password}");

            App.PressEnter();

            App.Screenshot("New User Created Using Enter Button");
        }
    }
}