namespace Tests.ViewModel
{
    using System.Linq;

    using NUnit.Framework;

    using TestDeTest.ViewModel;

    public class MainViewModelTest
    {
        private MainViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new MainViewModel();
        }


        [Test]
        public void UpdateDataInViewModelShouldUpdateInModelTest()
        {
            //arange


            //act


            //assert
        }

        [Test]
        public void AddData_AddsItemToTheCollection()
        {
            var name = "Scott";

            _viewModel.Name = name;
            _viewModel.Size = 100;
            _viewModel.AddCommand.Execute(null);

            var dataItem = _viewModel.Items.SingleOrDefault(i => i.Name == name);

            Assert.IsNotNull(dataItem, "Item was not added to the list");
        }

        [Test]
        public void RemoveData_RemovesItemToTheCollection()
        {
            var name = "Jack";

            var dataItem = _viewModel.Items.SingleOrDefault(i => i.Name == name);
            Assert.IsNotNull(dataItem, "Item was not in the list");

            _viewModel.SelectedItem = dataItem;

            _viewModel.RemoveCommand.Execute(null);

            var nullItem = _viewModel.Items.SingleOrDefault(i => i.Name == name);
            Assert.IsNull(nullItem, "Item was not removed from the list");
        }

        [Test]
        public void TotalSize_IsSetOnInitialization()
        {
            Assert.That(_viewModel.TotalSize, Is.EqualTo(700));
        }

        [Test]
        public void TotalSize_UpdatedWhenItemAdded()
        {
            AddData_AddsItemToTheCollection();

            Assert.That(_viewModel.TotalSize, Is.EqualTo(800));
        }

        [Test]
        public void TotalSize_UpdatedWhenItemRemoved()
        {
            RemoveData_RemovesItemToTheCollection();

            Assert.That(_viewModel.TotalSize, Is.EqualTo(400));
        }
    }
}