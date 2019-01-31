using System.ComponentModel;

namespace mlv_dump_ui.Models.BindingViews
{
    public class RawFileInfo : BaseEntity, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isselected = false;
        private string _name;
        private string _path;
        private string _createTime;
        private decimal _size;
        private string _taskProgress = "1";

        public bool IsSelected
        {
            get { return _isselected; }
            set
            {
                _isselected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }

        public string CreateTime
        {
            get { return _createTime; }
            set
            {
                _createTime = value;
                OnPropertyChanged("CreateTime");
            }
        }

        public decimal Size
        {
            get { return _size; }
            set
            {
                _size = value;
                OnPropertyChanged("Size");
            }
        }

        public string TaskProgress
        {
            get { return _taskProgress; }
            set
            {
                _taskProgress = value;
                OnPropertyChanged("TaskProgress");
            }
        }
        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return System.IO.Path.Combine(Path, Name);
        }
    }
}
