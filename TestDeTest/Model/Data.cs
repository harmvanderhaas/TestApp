namespace TestDeTest.Model
{
    public class Data
    {

        public Data(int id, string name, int size)
        {
            Id = id;
            Name = name;
            Size = size;
        }

        public string Name { get; private set; }

        public int Size { get; private set; }

        public int Id { get; private set; }

    }
}