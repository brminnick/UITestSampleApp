using Xamarin.Forms;

namespace UITestSampleApp
{
	public class WhiteTextImageCell : ImageCell
	{
		public WhiteTextImageCell() : base()
		{
			TextColor = Color.White;
			DetailColor = Color.White;

			this.SetValue(ImageCell.TextProperty, "TextProperty");
			this.SetBinding(ImageCell.DetailProperty, "DetailProperty");
			this.SetValue(ImageCell.ImageSourceProperty, "Hash");
		}
	}
}

