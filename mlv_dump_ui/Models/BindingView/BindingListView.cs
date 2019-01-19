using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mlv_dump_ui.Models.BindingView
{
    public class BindingListView<T> : INotifyPropertyChanged
    {
        private ObservableCollection<T> _bindingList = new ObservableCollection<T>();

        public ObservableCollection<T> BindingList
        {
            get { return _bindingList; }
            set
            {
                _bindingList = value;
                OnPropertyChanged("BindingList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
