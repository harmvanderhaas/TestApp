namespace TestDeTest.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Data;
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
            _model.DataAdded += ModelOnDataAdded;
            _model.DataRemoved += ModelOnDataRemoved;
            UpdateTotalSize();
        }

        private void ModelOnDataRemoved(object sender, DataRemovedArgs e)
        {
            DataViewModel data = Items.FirstOrDefault(i => i.Name == e.Name);
            Items.Remove(data);
            UpdateTotalSize();
        }

        private void ModelOnDataAdded(object sender, DataAddedArgs e)
        {
            Items.Add(new DataViewModel(e.Name, e.Size));
            UpdateTotalSize();
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

        private void UpdateTotalSize()
        {
            TotalSize = _model.GetTotalDataSize();
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