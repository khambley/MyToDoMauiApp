using System;
using MyToDoMauiApp.Models;

namespace MyToDoMauiApp.Repositories
{
	public interface ITodoItemRepository
	{
        event EventHandler<TodoItem> OnItemAdded;
        event EventHandler<TodoItem> OnItemUpdated;
        event EventHandler<TodoItem> OnItemDeleted;

        event EventHandler<ToDoList> OnListAdded;
        event EventHandler<ToDoList> OnListUpdated;
        event EventHandler<ToDoList> OnListDeleted;

        Task<List<TodoItem>> GetItemsAsync();
        Task AddItemAsync(TodoItem item);
        Task UpdateItemAsync(TodoItem item);
        Task AddOrUpdateAsync(TodoItem item);
        Task DeleteItemAsync(TodoItem item);

        Task<List<ToDoList>> GetListsAsync();
        Task AddListAsync(ToDoList list);
        Task UpdateListAsync(ToDoList list);
        Task AddOrUpdateListAsync(ToDoList list);
        Task DeleteListAsync(ToDoList list);

    }
}

