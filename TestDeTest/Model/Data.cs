namespace TestDeTest.Model
{
    public class Data
    {
        public Data(string name, int size)
        {
            Name = name;
            Size = size;
        }
        public string Name { get; private set; }

        public int Size { get; private set; }
    }
}