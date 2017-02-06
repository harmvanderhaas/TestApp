namespace TestDeTest.Model
{
    public class DataRemovedArgs
    {
        public DataRemovedArgs(string name)
        {
            Name = name;
        }

        public DataRemovedArgs(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}