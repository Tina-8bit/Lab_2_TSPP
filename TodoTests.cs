using Xunit;
using TodoApp;
using System;
using System.Linq;

namespace TodoApp.Tests
{
    public class ItemServiceTests
    {
        private readonly ItemService _service;

        public ItemServiceTests()
        {
            _service = new ItemService();
        }

 

        [Fact]
        public void Add_ValidTitle_ShouldAddItem()
        {
           
            var item = _service.Add("Task 1");
            Assert.NotNull(item);
            Assert.Equal("Task 1", item.Title);
        }

        [Fact]
        public void Add_EmptyTitle_ShouldThrowException()
        {
           
            Assert.Throws<ArgumentException>(() => _service.Add(""));
        }

       

        [Fact]
        public void Delete_ExistingItem_ShouldRemoveFromList()
        {
           
            var item = _service.Add("To delete");
            _service.Delete(item.Id);
            Assert.Empty(_service.GetAll());
        }

        [Fact]
        public void Delete_NonExistingId_ShouldThrowException()
        {
           
            Assert.Throws<Exception>(() => _service.Delete(999));
        }



        [Fact]
        public void Complete_ExistingItem_ShouldSetStatusToTrue()
        {
           
            var item = _service.Add("To complete");
            _service.Complete(item.Id);
            Assert.True(item.IsCompleted);
        }

        [Fact]
        public void Complete_InvalidId_ShouldThrowException()
        {
          
            Assert.Throws<Exception>(() => _service.Complete(888));
        }

     

        [Fact]
        public void GetCompleted_ShouldFilterItemsCorrectly()
        {
       
            _service.Add("Not done");
            var doneItem = _service.Add("Done");
            _service.Complete(doneItem.Id);

            var completedList = _service.GetCompleted();

            Assert.Single(completedList);
            Assert.True(completedList.All(t => t.IsCompleted));
        }

        [Fact]
        public void GetAll_ShouldReturnAllAddedItems()
        {
        
            _service.Add("Task A");
            _service.Add("Task B");

            var all = _service.GetAll();

            Assert.Equal(2, all.Count);
        }
    }
}