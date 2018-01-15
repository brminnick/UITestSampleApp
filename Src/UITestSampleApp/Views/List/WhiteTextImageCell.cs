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
		}
		#endregion

		#region Methods
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

            Text = string.Empty;
			Detail = string.Empty;

			var item = BindingContext as ListPageDataModel;

			Text = item?.Text ?? "";
            Detail = item?.Detail ?? "";
		}
		#endregion
	}
}