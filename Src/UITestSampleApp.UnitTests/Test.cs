using NUnit.Framework;
using System;
using UITestSampleApp.Shared;
namespace UITestSampleApp.UnitTests
{ 
    public class Test
    {
        [Test]
        public void ListPageDataModelTest()
        {
            //Arrange
            const int detail = 7;
            const string text = "Test";
            var model = new ListPageDataModel();

            //Act
            model.Detail = detail;
            model.Text = text;

            //Assert
            Assert.IsNotNull(model.Id);
            Assert.AreEqual(detail, model.Detail);
            Assert.AreEqual(text, model.Text);
        }
    }
}
