namespace TestDeTest.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DataModel
    {
        private int _lastId;

        private readonly List<Data> _data;

        public DataModel()
        {
            _lastId = 0;
            _data = new List<Data>();
            AddDataWithId("Jack", 300);
            AddDataWithId("Palinka", 400);
        }

        public event EventHandler<DataAddedArgs> DataAdded = (sender, args) => { };
        public event EventHandler<DataRemovedArgs> DataRemoved = (sender, args) => { };

        public List<Data> GetData()
        {
            return _data;
        }

        public void AddData(string name, int size)
        {
            var item = AddDataWithId(name, size);
            DataAdded.Invoke(this, new DataAddedArgs(item));
        }

        public void RemoveData(int id)
        {
            var itemToRemove = _data.SingleOrDefault(item => item.Id == id);
            if (itemToRemove == null)
                return;

            _data.Remove(itemToRemove);
            DataRemoved.Invoke(this, new DataRemovedArgs(id));
        }

        public int GetTotalDataSize()
        {
            return _data.Sum(item => item.Size);
        }


        private Data AddDataWithId(string name, int size)
        {
            var item = new Data(GetNextId(), name, size);
            _data.Add(item);
            return item;
        }


        private int GetNextId()
        {
            this._lastId++;
            return _lastId;
        }
    }
}