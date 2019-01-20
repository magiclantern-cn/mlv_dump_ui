using System.ComponentModel;

namespace mlv_dump_ui.Models.BindingViews
{
    public class CheckAllSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isselected = false;
    
        public bool IsSelected
        {
            get { return _isselected; }
            set
            {
                _isselected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
