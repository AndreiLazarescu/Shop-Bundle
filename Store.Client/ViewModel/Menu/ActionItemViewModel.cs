using Prism.Events;
using Store.Client.Events;
using System;
using System.Windows.Media;

namespace Store.Client.ViewModel.Menu
{
    public class ActionItemViewModel : MenuItemViewModel
    {
        private SolidColorBrush backgroundBrush;
        public SolidColorBrush BackgroundBrush
        {
            get => backgroundBrush;
            set => SetProperty(ref backgroundBrush, value);
        }

        private SolidColorBrush titleBrush;
        public SolidColorBrush TitleBrush
        {
            get => titleBrush;
            set => SetProperty(ref titleBrush, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public ActionItemViewModel(IEventAggregator eventAggregator, Type targetType)
            : base(eventAggregator, targetType) { }

        protected override void OnItemClick()
        {
            eventAggregator.GetEvent<TypeItemClickEvent>().Publish(targetType);
        }
    }
}
