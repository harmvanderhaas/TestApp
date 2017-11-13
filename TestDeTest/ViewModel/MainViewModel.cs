namespace TestDeTest.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Data;
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

        private void ModelOnDataAdded(object sender, DataAddedArgs dataAddedArgs)
        {
            DataViewModel dataViewModel = new DataViewModel(dataAddedArgs.Name, dataAddedArgs.Size);
            Items.Add(dataViewModel);
            RefreshCollection();
        }

        private void ModelOnDataRemoved(object sender, DataRemovedArgs dataRemovedArgs)
        {
            var dataViewModel = Items.FirstOrDefault(d => d.Name == dataRemovedArgs.Name);
            Items.Remove(dataViewModel);
            RefreshCollection();
        }

        private void RefreshCollection()
        {
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
            RefreshCollection();
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