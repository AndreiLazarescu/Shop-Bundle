using System.Windows;

namespace Store.Client.Controls
{
    public class MenuButton : ImageButton
    {
        public static readonly DependencyProperty IsMenuOpenProperty = DependencyProperty.Register(nameof(IsMenuOpen), typeof(bool), typeof(MenuButton));

        public bool IsMenuOpen
        {
            get => (bool)GetValue(IsMenuOpenProperty);
            set => SetValue(IsMenuOpenProperty, value);
        }

    }
}
