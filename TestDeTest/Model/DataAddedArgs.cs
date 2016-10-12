namespace TestDeTest.Model
{
    public class DataAddedArgs
    {
        public DataAddedArgs(string name, int size)
        {
            Name = name;
            Size = size;
        }

        public int Size { get; set; }

        public string Name { get; set; }
    }
}