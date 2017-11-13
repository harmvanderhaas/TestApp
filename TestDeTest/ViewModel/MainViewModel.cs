namespace TestDeTest.ViewModel
{
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
            _model.DataAdded += ModelOnDataAdded;
            _model.DataRemoved += ModelOnDataRemoved;
           
            InitializeCollection();
            AddCommand = new Command(Add, _ => true);
            RemoveCommand = new Command(Remove, _ => true);
        }

        private void ModelOnDataRemoved(object sender, DataRemovedArgs dataRemovedArgs)
        {
            string name = dataRemovedArgs.Name;
            
            _model.RemoveData(name);
            var item = Items.SingleOrDefault(i => i.Name == name);
            if (item != null)
                Items.Remove(item);

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
            _model.AddData(Name,Size);

        }

        private void ModelOnDataAdded(object sender, DataAddedArgs dataAddedArgs)
        {

            DataViewModel test = new DataViewModel(dataAddedArgs.Name, dataAddedArgs.Size);
            Items.Add(test);

            TotalSize = _model.GetTotalDataSize();

        }

        private void Remove(object obj)
        {

            string name = SelectedItem.Name;
            
            _model.RemoveData(name);

        }

        private void InitializeCollection()
        {
            var dataViewModels = _model.GetData().Select(d => new DataViewModel(d.Name, d.Size));
            TotalSize = _model.GetTotalDataSize();
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