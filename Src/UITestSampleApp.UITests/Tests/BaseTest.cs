using System;
using NUnit.Framework;

using Xamarin.UITest;

namespace UITestSampleApp.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    abstract class BaseTest
    {
        readonly Platform _platform;

        IApp? _app;
        FirstPage? _firstPage;
        ListPage? _listPage;
        LoginPage? _loginPage;
        NewUserSignUpPage? _newUserSignUpPage;

        protected BaseTest(Platform platform) => _platform = platform;

        protected IApp App => _app ?? throw new NullReferenceException();
        protected FirstPage FirstPage => _firstPage ?? throw new NullReferenceException();
        protected ListPage ListPage => _listPage ?? throw new NullReferenceException();
        protected LoginPage LoginPage => _loginPage ?? throw new NullReferenceException();
        protected NewUserSignUpPage NewUserSignUpPage => _newUserSignUpPage ?? throw new NullReferenceException();

        [SetUp]
        public virtual void BeforeEachTest()
        {
            _app = AppInitializer.StartApp(_platform);

            _firstPage = new FirstPage(App);
            _listPage = new ListPage(App);
            _loginPage = new LoginPage(App);
            _newUserSignUpPage = new NewUserSignUpPage(App);

            App.Screenshot("App Initialized");
            LoginPage.WaitForPageToLoad();
        }
    }
}

