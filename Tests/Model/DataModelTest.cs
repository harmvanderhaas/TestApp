namespace Tests.Model
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
        public void UpdateDataShouldUpdateDateInCollectionTest()
        {
            //arrange
            // select Jack and update name to Coen
            var origName = "Jack";
            var name = "Coen";

            //act
            var dataItem = _model.GetData().SingleOrDefault(d => d.Name == origName);
           // var newItem = new UpdateData(dataItem, name, dataItem.Size);

        //    _model.Update(newItem);

            //assert
            

        }



        [Test]
        public void AddData_AddsItemToTheCollection()
        {
            var name = "Scott";
            var size = 100;
            var id = -1;
            _model.DataAdded += (sender, e) =>
                                {
                                    id = e.Id;
                                };
            _model.AddData(name, size);

            var dataItem = _model.GetData().SingleOrDefault(d => d.Name == name);

            Assert.IsNotNull(dataItem, "Item was not added to the list list");
            Assert.That(dataItem.Size, Is.EqualTo(size));
            Assert.That(id, Is.Not.EqualTo(-1));
        }

        [Test]
        public void RemoveData_RemovesItemToTheCollection()
        {
            var name = "Jack";
            var id = 1;

            _model.RemoveData(id);

            var dataItem = _model.GetData().SingleOrDefault(d => d.Id == id);

            Assert.IsNull(dataItem, "Item was not removed from the list list");
        }

        [Test]
        public void GetTotalDataSize_ReturnsTotalCountOfSizeOfTheCollection()
        {
            var id = 1;
            var totalSize = _model.GetTotalDataSize();
            Assert.That(totalSize, Is.EqualTo(700),  "Size of the items was not calculated correct");

            _model.RemoveData(id);

            var totalSizeAfterRemove = _model.GetTotalDataSize();
            Assert.That(totalSizeAfterRemove, Is.EqualTo(400), "Size of the items was not calculated correct");

            var name2 = "Scott";
            var size2 = 100;
            _model.AddData(name2, size2);

            var totalSizeAfterAdd = _model.GetTotalDataSize();
            Assert.That(totalSizeAfterAdd, Is.EqualTo(500), "Size of the items was not calculated correct");
        }
    }
}
