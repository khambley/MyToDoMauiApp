using System;
using MyToDoMauiApp.Models;

namespace MyToDoMauiApp.Repositories
{
	public interface ITodoItemRepository
	{
        event EventHandler<TodoItem> OnItemAdded;
        event EventHandler<TodoItem> OnItemUpdated;

        Task<List<TodoItem>> GetItemsAsync();
        Task AddItemAsync(TodoItem item);
        Task UpdateItemAsync(TodoItem item);
        Task AddOrUpdateAsync(TodoItem item);
    }
}

