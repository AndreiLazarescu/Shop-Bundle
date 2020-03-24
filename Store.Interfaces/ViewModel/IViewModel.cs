using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Store.Interfaces.ViewModel
{
    public interface IViewModel : INotifyPropertyChanged
    {
        string Title { get; set; }
    }
}
