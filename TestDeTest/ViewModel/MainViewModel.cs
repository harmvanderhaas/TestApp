namespace TestDeTest.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using TestDeTest.Model;

    public class MainViewModel : ViewModel
    {
        private readonly DataModel _model;
        private int _totalSize;

        public MainViewModel()
        {
            _model = new DataModel();
            InitializeCollection();
            AddCommand = new Command(Add, _ => true);
            RemoveCommand = new Command(Remove, _ => true);

            _model.DataAdded += _model_DataAdded;
            _model.DataRemoved += _model_DataRemoved;
            TotalSize = _model.GetTotalDataSize();
        }

        private void _model_DataRemoved(object sender, DataRemovedArgs e)
        {
            if (Items.Any(x => x.Name.Equals(e.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                Items.Remove(SelectedItem);
                TotalSize = _model.GetTotalDataSize();
            }
        }

        private void _model_DataAdded(object sender, DataAddedArgs e)
        {
            Items.Add(new DataViewModel(e.Name, e.Size));
            TotalSize = _model.GetTotalDataSize();
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
            _model.RemoveData(SelectedItem.Name);
        }

        private void InitializeCollection()
        {
            var dataViewModels = _model.GetData().Select(d => new DataViewModel(d.Name, d.Size));
            Items = new ObservableCollection<DataViewModel>(dataViewModels);
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
    }
}