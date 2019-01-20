using System.ComponentModel;

namespace mlv_dump_ui.Models.BindingView
{
    //public class MLVFileInfo:BaseEntity, INotifyPropertyChanged
    //{
    //    public bool Select { get; set; } = false;
    //    public string Name { get; set; }
    //    public string Path { get; set; }
    //    public string CreateTime { get; set; }
    //    public decimal Size { get; set; }
    //    public string TaskProgress { get; set; }

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public override string ToString()
    //    {
    //        return System.IO.Path.Combine(Path, Name);
    //    }
    //}
    public class MLVFileInfo : BaseEntity, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _select = false;
        private string _name;
        private string _path;
        private string _createTime;
        private decimal _size;
        private string _taskProgress = "1";

        public bool Select
        {
            get { return _select; }
            set
            {
                _select = value;
                OnPropertyChanged("Select");
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
