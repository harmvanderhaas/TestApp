namespace TestDeTest.Model
{
    public class Data
    {
        public Data(string name, int size, int value)
        {
            Name = name;
            Size = size;
            Value = value;
        }
        public string Name { get; private set; }

        public int Size { get; private set; }

        public int Value { get; private set; }
    }
}