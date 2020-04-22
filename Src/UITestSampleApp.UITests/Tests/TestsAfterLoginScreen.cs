using System.Collections.Generic;
using NUnit.Framework;
using UITestSampleApp.Shared;
using Xamarin.UITest;

namespace UITestSampleApp.UITests
{
    class TestsAfterLoginScreen : BaseTest
    {
        public TestsAfterLoginScreen(Platform platform) : base(platform)
        {
        }

        public override void BeforeEachTest()
        {
            base.BeforeEachTest();

            LoginPage.WaitForPageToLoad();

            App.InvokeBackdoorMethod(BackdoorMethodConstants.BypassLoginScreen);

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
            const int listItemNumber = 9;
            var expectedAlertString = $"You Selected Number {listItemNumber}";

            //Act
            App.InvokeBackdoorMethod(BackdoorMethodConstants.OpenListViewPage);

            ListPage.WaitForPageToLoad();
            ListPage.WaitForNoActivityIndicator();

            ListPage.TapListItemNumber(listItemNumber);
            var actualAlertString = ListPage.GetAlertText(listItemNumber);

            ListPage.TapOKOnAlert();

            //Assert
            Assert.AreEqual(expectedAlertString, actualAlertString);
            Assert.GreaterOrEqual(App.InvokeBackdoorMethod<IReadOnlyList<ListPageDataModel>>(BackdoorMethodConstants.GetListViewPageData).Count, 10, "Less than 10 items found in List");
        }
    }
}

