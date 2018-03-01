namespace UITestSampleApp.Shared
{
    public static class AutomationIdConstants
    {
        #region FirstPage
        public const string FirstPage_GoButton = nameof(FirstPage_GoButton);
        public const string FirstPage_TextEntry = nameof(FirstPage_TextEntry);
        public const string FirstPage_TextLabel = nameof(FirstPage_TextLabel);
        public const string FirstPage_ListViewButton = nameof(FirstPage_ListViewButton);
        public const string FirstPage_BusyActivityIndicator = nameof(FirstPage_BusyActivityIndicator);
        #endregion

        #region LoginPage 
        public const string LoginPage_UsernameEntry = nameof(LoginPage_UsernameEntry);
        public const string LoginPage_PasswordEntry = nameof(LoginPage_PasswordEntry);
        public const string LoginPage_LoginButton = nameof(LoginPage_LoginButton);
        public const string LoginPage_NewUserSignUpButton = nameof(LoginPage_NewUserSignUpButton);
        public const string LoginPage_ForgotPasswordButton = nameof(LoginPage_ForgotPasswordButton);
        public const string LoginPage_CrashButton = nameof(LoginPage_CrashButton);
        #endregion

        #region NewUserSignUpPage
        public const string NewUserSignUpPage_NewUserNameEntry = nameof(NewUserSignUpPage_NewUserNameEntry);
        public const string NewUserSignUpPage_NewPasswordEntry = nameof(NewUserSignUpPage_NewPasswordEntry);
        public const string NewUserSignUpPage_SaveUsernameButton = nameof(NewUserSignUpPage_SaveUsernameButton);
        public const string NewUserSignUpPage_CancelButton = nameof(NewUserSignUpPage_CancelButton);
        #endregion
    }
}

