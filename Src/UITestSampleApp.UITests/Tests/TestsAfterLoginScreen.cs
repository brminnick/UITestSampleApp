using NUnit.Framework;

using Xamarin.UITest;

namespace UITestSampleApp.UITests
{
    [Category(nameof(TestsAfterLoginScreen))]
    class TestsAfterLoginScreen : BaseTest
    {
        public TestsAfterLoginScreen(Platform platform) : base(platform)
        {
        }

        public override void BeforeEachTest()
        {
            base.BeforeEachTest();

            LoginPage.WaitForPageToLoad();

            BackdoorHelpers.BypassLoginScreen(App);

            FirstPage.WaitForPageToLoad();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void EnterText(bool shouldUseKeyboardReturnButton)
        {
            //Arrange
            var textInput = "Hello World";

            //Act
            switch (shouldUseKeyboardReturnButton)
            {
                case true:
                    FirstPage.EnterTextAndPressEnter(textInput);
                    break;
                case false:
                    FirstPage.EnterText(textInput);
                    FirstPage.ClickGo();
                    break;
            }

            FirstPage.WaitForNoActivityIndicator();

            //Assert
            Assert.AreEqual(FirstPage.GetEntryFieldText(), textInput);
        }

        [Test]
        public void VerifyItemsInListView()
        {
            //Arrange
            var listItemNumber = 9;
            var expectedAlertString = $"You Selected Number {listItemNumber}";

            //Act
            BackdoorHelpers.OpenListViewPage(App);

            ListPage.WaitForPageToLoad();
            ListPage.WaitForNoActivityIndicator();

            //Assert
            Assert.IsTrue(BackdoorHelpers.GetListPageData(App)?.Count >= 30, "Less than 30 items found in List");
        }
    }
}

