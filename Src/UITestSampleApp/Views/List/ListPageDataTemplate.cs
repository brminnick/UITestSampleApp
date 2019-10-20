﻿using UITestSampleApp.Shared;
using Xamarin.Forms;

namespace UITestSampleApp
{
    public class ListPageDataTemplate : DataTemplate
    {
        public ListPageDataTemplate() : base(CreateDataTemplate)
        {
        }

        static Grid CreateDataTemplate()
        {
            var image = new Image { Source = "Hash" };

            var titleLabel = new WhiteLabel(14)
            {
                FontAttributes = FontAttributes.Bold
            };
            titleLabel.SetBinding(Label.TextProperty, nameof(ListPageDataModel.Text));

            var detailLabel = new WhiteLabel(11);
            detailLabel.SetBinding(Label.TextProperty, nameof(ListPageDataModel.Detail));

            var grid = new Grid
            {
                Margin = new Thickness(10,0),
                RowSpacing = 10,

                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(20, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(15, GridUnitType.Absolute) },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) }
                }
            };

            grid.Children.Add(image, 0, 0);
            Grid.SetRowSpan(image, 2);

            grid.Children.Add(titleLabel, 1, 0);
            grid.Children.Add(detailLabel, 1, 1);

            return grid;
        }

        class WhiteLabel : Label
        {
            public WhiteLabel(double fontSize)
            {
                FontSize = fontSize;
                TextColor = Color.White;
            }
        }
    }
}
