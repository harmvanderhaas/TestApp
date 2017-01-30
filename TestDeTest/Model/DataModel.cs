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
            var data = _data.SingleOrDefault(a => a.Name == name);

            if (data == null)
                return;

            var removed = _data.Remove(data);
            if (!removed)
                return;

            DataRemoved.Invoke(this, new DataRemovedArgs(name));
        }

        public int GetTotalDataSize()
        {
            var size = _data.Sum(a => a.Size);

            return size;
            //throw new NotImplementedException();
        }
    }
}