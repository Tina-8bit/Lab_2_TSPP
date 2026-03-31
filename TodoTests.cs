using Xunit;
using TodoApp;
using System;
using System.Collections.Generic;

namespace TodoApp.Tests
{
    public class TodoTests
    {
        private readonly ItemService _service;

        public TodoTests()
        {
            _service = new ItemService();
        }

        
        [Fact]
        public void Add_ValidTitle_ShouldAddItem()
        {
            var title = "Test task";
            var item = _service.Add(title);

            Assert.NotNull(item);
            Assert.Equal(title, item.Title); 
        }

        [Fact]
        public void Add_EmptyTitle_ShouldThrowArgumentException()
        {
            
            Assert.Throws<ArgumentException>(() => _service.Add(""));
        }

        
        [Fact]
        public void Delete_ExistingId_ShouldRemoveItem()
        {
            var item = _service.Add("Task to delete");

            _service.Delete(item.Id);

            Assert.DoesNotContain(_service.GetAll(), x => x.Id == item.Id);
        }

        [Fact]
        public void Delete_NonExistingId_ShouldThrowException()
        {
           
            Assert.Throws<Exception>(() => _service.Delete(9999));
        }

        
        [Fact]
        public void Complete_ValidId_ShouldSetIsCompletedToTrue()
        {
            
            var item = _service.Add("New Task");

            
            _service.Complete(item.Id); 

            
            Assert.True(item.IsCompleted); 
        }

        [Fact]
        public void Complete_InvalidId_ShouldThrowException()
        {
            
            Assert.Throws<Exception>(() => _service.Complete(-1));
        }
    }
}