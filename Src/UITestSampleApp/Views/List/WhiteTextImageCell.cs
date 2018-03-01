using Xamarin.Forms;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
    public class WhiteTextImageCell : ImageCell
    {
        #region Constructors
        public WhiteTextImageCell()
        {
            TextColor = Color.White;
            DetailColor = Color.White;
            ImageSource = "Hash";

            this.SetBinding(DetailProperty, nameof(ListPageDataModel.Detail));
            this.SetBinding(TextProperty, nameof(ListPageDataModel.Text));
        }
        #endregion
    }
}