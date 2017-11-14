namespace TestDeTest.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using TestDeTest.Model;

    public class MainViewModel : ViewModel , ICallBack
    {
        private readonly DataModel _model;
        private int _totalSize;

        public event EventHandler DataAddedEvent;

        

        public MainViewModel()
        {
            _model = new DataModel(this);

            InitializeCollection();
            AddCommand = new Command(Add, _ => true);
            RemoveCommand = new Command(Remove, _ => true);
          //  DataAddedEvent += new EventHandler(_model.DataAdded());

        }

        public Command RemoveCommand { get; set; }

        public ICommand AddCommand { get; set; }

        public ObservableCollection<DataViewModel> Items { get; set; }

        public DataViewModel SelectedItem { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public int TotalSize
        {
            get { return _totalSize; }
            set
            {
                _totalSize = value;
                OnPropertyChanged();
            }
        }


        private void Add(object obj)
        {
            _model.AddData(Name, Size);
        }

        private void Remove(object obj)
        {
            if (SelectedItem != null)
            {
                _model.RemoveData(SelectedItem.Name);
            }
        }

        private void InitializeCollection()
        {
            var dataViewModels = _model.GetData().Select(d => new DataViewModel(d.Name, d.Size));
            Items = new ObservableCollection<DataViewModel>(dataViewModels);
            UpdateTotalSize();
        }

        private void UpdateTotalSize()
        {
            TotalSize = _model.GetTotalDataSize();
        }

        public void ItemAdd(string name, int size)
        {
            Items.Add(new DataViewModel(name, size));
            UpdateTotalSize();
        }
    }

    public class DataViewModel : ViewModel
    {
        public DataViewModel(string name, int size)
        {
            Name = name;
            Size = size;
        }

        public string Name { get; set; }

        public int Size { get; set; }

        public bool ItemAdded { set; get; }
        public bool ItemRemoved { set; get; }
    }
}