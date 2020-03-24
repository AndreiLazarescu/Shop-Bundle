
using System.Windows;
using System.Windows.Media;

namespace Store.Client.Controls
{
    public class CardButton : ImageButton
    {
        public readonly static DependencyProperty DescriptionProperty = DependencyProperty.Register(nameof(Description), typeof(string), typeof(CardButton));
        public readonly static DependencyProperty TitleBrushProperty = DependencyProperty.Register(nameof(TitleBrush), typeof(SolidColorBrush), typeof(CardButton));

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public SolidColorBrush TitleBrush
        {
            get => (SolidColorBrush)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }
    }
}
