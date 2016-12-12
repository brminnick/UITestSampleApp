using Xamarin.Forms;

namespace UITestSampleApp
{
	public class WhiteTextImageCell : ImageCell
	{
		#region Constructors
		public WhiteTextImageCell()
		{
			TextColor = Color.White;
			DetailColor = Color.White;
		}
		#endregion

		#region Methods
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			Text = "";
			Detail = "";
			ImageSource = null;

			var item = BindingContext as ListPageDataModel;

			Text = item?.TextProperty ?? "";
			Detail = item?.DetailProperty ?? "";
			ImageSource = "Hash";
		}
		#endregion
	}
}