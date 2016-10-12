namespace Tests
{
    using System.Linq;

    using NUnit.Framework;

    using TestDeTest.Model;

    [TestFixture]
    public class DataModelTest
    {
        private DataModel _model;

        [SetUp]
        public void Setup()
        {
            _model = new DataModel();
        }
        
        [Test]
        public void AddData_AddsItemToTheCollection()
        {
            var name = "Scott";
            var size = 100;
            _model.AddData(name, size);

            var dataItem = _model.GetData().SingleOrDefault(d => d.Name == name);

            Assert.IsNotNull(dataItem, "Item was not added to the list list");
            Assert.That(dataItem.Size, Is.EqualTo(size));
        }

        [Test]
        public void RemoveData_RemovesItemToTheCollection()
        {
            var name = "Jack";

            _model.RemoveData(name);

            var dataItem = _model.GetData().SingleOrDefault(d => d.Name == name);

            Assert.IsNull(dataItem, "Item was not removed from the list list");
        }

        [Test]
        public void GetTotalDataSize_ReturnsTotalCountOfSizeOfTheCollection()
        {
           var totalSize = _model.GetTotalDataSize();

            Assert.That(totalSize, Is.EqualTo(700),  "Size of the items was not calculated correct");
        }
    }
}
