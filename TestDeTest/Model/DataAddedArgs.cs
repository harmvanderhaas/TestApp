namespace TestDeTest.Model
{
    public class DataAddedArgs
    {
        public DataAddedArgs(string name, int size, int value)
        {
            Name = name;
            Size = size;
            Value = value;
        }

        public int Size { get; private set; }

        public int Value { get; private set; }

        public string Name { get; private set; }
    }
}