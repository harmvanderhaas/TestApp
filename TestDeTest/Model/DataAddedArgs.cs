namespace TestDeTest.Model
{
    public class DataAddedArgs
    {
        public DataAddedArgs(Data item)
        {
            this.Name = item.Name;
            this.Size = item.Size;
            this.Id = item.Id;
        }

        public int Id { get; set; }

        public int Size { get; set; }

        public string Name { get; set; }
    }
}