namespace TestDeTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DataModel
    {
        private readonly List<Data> _data;

        public DataModel()
        {
            _data = new List<Data>()
                        {
                            new Data("Jack", 300),
                            new Data("Palinka", 400),
                        };
        }

        public event EventHandler<DataAddedArgs> DataAdded = (sender, args) => { };
        public event EventHandler<DataRemovedArgs> DataRemoved = (sender, args) => { };

        public List<Data> GetData()
        {
            return _data;
        }

        public void AddData(string name, int size)
        {
            _data.Add(new Data(name, size));
            DataAdded.Invoke(this, new DataAddedArgs(name, size));
        }

        public void RemoveData(string name)
        {
            if (_data.Any(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
            {
                _data.RemoveAll(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                DataRemoved.Invoke(this, new DataRemovedArgs(name));
            }
        }

        public int GetTotalDataSize()
        {
            return _data.Sum(x => x.Size);
        }
    }
}