﻿namespace Tests.Model
{
    using System.Collections.Generic;
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
            var name = "Jack";
            var totalSize = _model.GetTotalDataSize();
            Assert.That(totalSize, Is.EqualTo(700),  "Size of the items was not calculated correct");

            _model.RemoveData(name);

            var totalSizeAfterRemove = _model.GetTotalDataSize();
            Assert.That(totalSizeAfterRemove, Is.EqualTo(400), "Size of the items was not calculated correct");

            var name2 = "Scott";
            var size2 = 100;
            _model.AddData(name2, size2);

            var totalSizeAfterAdd = _model.GetTotalDataSize();
            Assert.That(totalSizeAfterAdd, Is.EqualTo(500), "Size of the items was not calculated correct");
        }

        [Test]
        public void AddData_DataAddedEvent_AddedItemInformationIsCorrect()
        {
            //arrange
            var eventsList = new List<DataAddedArgs>();
            _model.DataAdded += (sender, args) =>
                                {
                                    eventsList.Add(args);
                                };

            //act
            var paul = "Paul";
            var size = 200;
            _model.AddData(paul, size);
            
            //assert
            Assert.IsNotNull(eventsList.Select(d => d.Name == paul && d.Size == size));
        }

        [Test]
        public void RemoveData_DataRemovedEvent_RemovedItemInformationIsCorrect()
        {
            var eventsList = new List<DataRemovedArgs>();
            _model.DataRemoved += (sender, args) =>
                                  {
                                      eventsList.Add(args);
                                  };
            var name = "Jack";
            _model.RemoveData(name);

            Assert.IsNotNull(eventsList.Select(d => d.Name == name));
        }
    }
}
