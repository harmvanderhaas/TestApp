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
            _model.DataAdded += ModelOnDataAdded;
            _model.DataRemoved += ModelOnDataRemoved;
            AddCommand = new Command(Add, _ => true);
            RemoveCommand = new Command(Remove, _ => true);
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

        private void ModelOnDataAdded(object sender, DataAddedArgs dataAddedArgs)
        {
            Items.Add(new DataViewModel(dataAddedArgs.Name, dataAddedArgs.Size, dataAddedArgs.Value));
            TotalSize = _model.GetTotalDataSize();
        }
        private void ModelOnDataRemoved(object sender, DataRemovedArgs dataRemovedArgs)
        {
            TotalSize = _model.GetTotalDataSize();
            var data = Items.SingleOrDefault(a => a.Name == dataRemovedArgs.Name);

            if (data != null)
            {
                Items.Remove(data);
            }
        }

        private void Add(object obj)
        {
            _model.AddData(Name, Size, 0);
            //throw new System.NotImplementedException();
        }

        private void Remove(object obj)
        {
            if (SelectedItem != null)
            {
                _model.RemoveData(SelectedItem.Name);
            }
            //throw new System.NotImplementedException();
        }

        private void InitializeCollection()
        {
            var dataViewModels = _model.GetData().Select(d => new DataViewModel(d.Name, d.Size, d.Value));
            Items = new ObservableCollection<DataViewModel>(dataViewModels);
        }
    }

    public class DataViewModel : ViewModel
    {
        public DataViewModel(string name, int size, int value)
        {
            Name = name;
            Size = size;
            Value = value;
        }

        public string Name { get; set; }

        public int Size { get; set; }

        public int Value { get; set; }
    }
}