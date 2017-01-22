using System;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using MyLoginUI.Views;

using UITestSampleApp.iOS;

using UIKit;
using CoreAnimation;
using CoreGraphics;
using Foundation;

[assembly: ExportRenderer(typeof(StyledEntry), typeof(StyledEntryRenderer))]

namespace UITestSampleApp.iOS
{
	public class StyledEntryRenderer : EntryRenderer
	{
		UITextField nativeTextField;
		CALayer bottomBorder;
		bool isInitialized = false;

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == "IsEnabled")
			{
				if (!nativeTextField.Enabled)
					nativeTextField.TextColor = UIColor.White;
				else
					nativeTextField.TextColor = UIColor.Blue;
			}
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null && !isInitialized)
			{
				var formsEntry = e.NewElement as StyledEntry;
				nativeTextField = Control as UITextField;
				nativeTextField.Font = UIFont.FromName("AppleSDGothicNeo-Light", 18);
				nativeTextField.TextColor = UIColor.White;
				nativeTextField.BorderStyle = UITextBorderStyle.None;

				if (!String.IsNullOrEmpty(formsEntry.Placeholder))
					nativeTextField.AttributedPlaceholder = new NSAttributedString(formsEntry.Placeholder, UIFont.FromName("AppleSDGothicNeo-Light", 18), UIColor.White);

				bottomBorder = new CALayer();
				bottomBorder.BackgroundColor = UIColor.White.CGColor;
				Control.Layer.AddSublayer(bottomBorder);

				isInitialized = true;
			}

			var customEntry = Element as StyledEntry;

			if (Control != null && customEntry != null)
			{
				SetKeyboardButtonType(customEntry.ReturnType);

				Control.ShouldReturn += (UITextField tf) =>
				{
					customEntry?.InvokeCompleted();
					return true;
				};
			}
		}

		void SetKeyboardButtonType(ReturnType returnType)
		{
			switch (returnType)
			{
				case ReturnType.Go:
					Control.ReturnKeyType = UIReturnKeyType.Go;
					break;
				case ReturnType.Next:
					Control.ReturnKeyType = UIReturnKeyType.Next;
					break;
				case ReturnType.Send:
					Control.ReturnKeyType = UIReturnKeyType.Send;
					break;
				case ReturnType.Search:
					Control.ReturnKeyType = UIReturnKeyType.Search;
					break;
				case ReturnType.Done:
					Control.ReturnKeyType = UIReturnKeyType.Done;
					break;
				default:
					Control.ReturnKeyType = UIReturnKeyType.Default;
					break;
			}
		}

		public override CGSize SizeThatFits(CGSize size)
		{
			bottomBorder.Frame = new CGRect(0, size.Height - 1, size.Width, 1);
			return base.SizeThatFits(size);
		}
	}
}