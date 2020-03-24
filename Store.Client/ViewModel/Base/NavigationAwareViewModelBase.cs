using Prism.Mvvm;
using Prism.Regions;
using System;

namespace Store.Client.ViewModel.Base
{
    public abstract class NavigationAwareViewModelBase : ViewModelBase, INavigationAware
    {
        private string viewName;
        protected string ViewName
        {
            get
            {
                if (string.IsNullOrEmpty(viewName))
                {
                    viewName = GetType().Name.Replace("Model", string.Empty);
                }

                return viewName;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (string.Equals(navigationContext.Uri.ToString(), ViewName))
            {
                return true;
            }

            return false;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }


    }
}
