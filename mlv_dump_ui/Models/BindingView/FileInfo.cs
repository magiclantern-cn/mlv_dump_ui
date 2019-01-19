namespace mlv_dump_ui.Models.BindingView
{
    public class MLVFileInfo:BaseEntity
    {
        public bool Select { get; set; } = false;
        public string Name { get; set; }
        public string Path { get; set; }
        public string CreateTime { get; set; }
        public decimal Size { get; set; }
        public string TaskProgress { get; set; }
        public override string ToString()
        {
            return System.IO.Path.Combine(Path, Name);
        }
    }
    //public class MLVFileInfo : BaseEntity, INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    private bool _select = false;
    //    private string _name;
    //    private string _path;
    //    private string _createTime;
    //    private decimal _size;
    //    private string _taskProgress = "1";

    //    public bool Select
    //    {
    //        get { return _select; }
    //        set
    //        {
    //            _select = value;
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Select.ToString()));
    //        }
    //    }

    //    public string Name
    //    {
    //        get { return _name; }
    //        set
    //        {
    //            _name = value;
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
    //        }
    //    }

    //    public string Path
    //    {
    //        get { return _path; }
    //        set
    //        {
    //            _path = value;
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Path));
    //        }
    //    }

    //    public string CreateTime
    //    {
    //        get { return _createTime; }
    //        set
    //        {
    //            _createTime = value;
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(CreateTime));
    //        }
    //    }

    //    public decimal Size
    //    {
    //        get { return _size; }
    //        set
    //        {
    //            _size = value;
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Size.ToString()));
    //        }
    //    }

    //    public string TaskProgress
    //    {
    //        get { return _taskProgress; }
    //        set
    //        {
    //            _taskProgress = value;
    //            if (PropertyChanged != null)
    //            {
    //                PropertyChanged(this, new PropertyChangedEventArgs(TaskProgress));
    //            }
    //            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(TaskProgress));
    //        }
    //    }
    //    public override string ToString()
    //    {
    //        return System.IO.Path.Combine(Path, Name);
    //    }
    //}
}
