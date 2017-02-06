namespace TestDeTest.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime;
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
            UpdateCommmand = new Command(Update, _ => true);

            _model.DataAdded += ModelOnDataAdded;
            _model.DataRemoved += ModelOnDataRemoved;

            TotalSize = this._model.GetTotalDataSize();
        }

        private void Update(object obj)
        {
            throw new NotImplementedException();
        }

        private void ModelOnDataRemoved(object sender, DataRemovedArgs dataRemovedArgs)
        {
            var itemToRemove = Items.FirstOrDefault(item => item.Id == dataRemovedArgs.Id);
            if (itemToRemove == null)
                return;

            Items.Remove(itemToRemove);
            TotalSize = this._model.GetTotalDataSize();
        }

        private void ModelOnDataAdded(object sender, DataAddedArgs dataAddedArgs)
        {
            Items.Add(new DataViewModel(dataAddedArgs.Id, dataAddedArgs.Name, dataAddedArgs.Size));
            TotalSize = this._model.GetTotalDataSize();
        }

        public Command RemoveCommand { get; set; }

        public ICommand AddCommand { get; set; }

        public ICommand UpdateCommmand { get; set; }

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
            _model.RemoveData(SelectedItem.Id);
        }

        private void InitializeCollection()
        {
            var dataViewModels = _model.GetData().Select(d => new DataViewModel(d.Id, d.Name, d.Size));
            Items = new ObservableCollection<DataViewModel>(dataViewModels);
        }
    }

    public class DataViewModel : ViewModel
    {
        public DataViewModel(int id, string name, int size)
        {
            Id = id;
            Name = name;
            Size = size;
        }

        public string Name { get; set; }

        public int Size { get; set; }

        public int Id { get; set; }
    }
}