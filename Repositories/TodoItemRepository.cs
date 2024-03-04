using System;
using MyToDoMauiApp.Models;
using SQLite;

namespace MyToDoMauiApp.Repositories
{
	public class TodoItemRepository : ITodoItemRepository
	{
        private SQLiteAsyncConnection connection;

        public event EventHandler<TodoItem> OnItemAdded;
        public event EventHandler<TodoItem> OnItemUpdated;
        public event EventHandler<TodoItem> OnItemDeleted;

        public event EventHandler<ToDoList> OnListAdded;
        public event EventHandler<ToDoList> OnListUpdated;
        public event EventHandler<ToDoList> OnListDeleted;

        private async Task CreateConnectionAsync()
        {
            if(connection != null)
            {
                return;
            }

            // doesn't work on Android -> Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var documentPath = FileSystem.AppDataDirectory; 

            var databasePath = Path.Combine(documentPath, "TodoItems.db");

            connection = new SQLiteAsyncConnection(databasePath);

            await connection.CreateTableAsync<TodoItem>();

            if (await connection.Table<TodoItem>().CountAsync() == 0)
            {
                await connection.InsertAsync(new TodoItem()
                {
                    Title = "Welcome to DayByDayTasks App",
                    Due = DateTime.UtcNow
                });
            }

            await connection.CreateTableAsync<ToDoList>();

            if (await connection.Table<ToDoList>().CountAsync() == 0)
            {
                await connection.InsertAsync(new ToDoList()
                {
                    DateName = DateTime.Now,
                    TodoItems = await GetItemsAsync()
                });
            }
        }

        #region ToDo Items
        
        public async Task AddItemAsync(TodoItem item)
        {
            await CreateConnectionAsync();
            await connection.InsertAsync(item);
            OnItemAdded?.Invoke(this, item); //notify any subscribers
        }

        public async Task DeleteItemAsync(TodoItem item)
        {
            await CreateConnectionAsync();
            await connection.DeleteAsync(item);
            OnItemDeleted?.Invoke(this, item);
        }

        public async Task AddOrUpdateAsync(TodoItem item)
        {
            if (item.Id == 0)
            {
                await AddItemAsync(item);
            }
            else
            {
                await UpdateItemAsync(item);
            }
        }

        public async Task<List<TodoItem>> GetItemsAsync()
        {
            await CreateConnectionAsync();
            return await connection.Table<TodoItem>().ToListAsync();
        }

        public async Task UpdateItemAsync(TodoItem item)
        {
            await CreateConnectionAsync();
            await connection.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }

        #endregion

        #region ToDoLists

        public async Task AddListAsync(ToDoList list)
        {
            await CreateConnectionAsync();
            await connection.InsertAsync(list);
            OnListAdded?.Invoke(this, list); //notify any subscribers
        }

        public async Task DeleteListAsync(ToDoList list)
        {
            await CreateConnectionAsync();
            await connection.DeleteAsync(list);
            OnListDeleted?.Invoke(this, list);
        }

        public async Task AddOrUpdateListAsync(ToDoList list)
        {
            if (list.Id == 0)
            {
                await AddListAsync(list);
            }
            else
            {
                await UpdateListAsync(list);
            }
        }

        public async Task<List<ToDoList>> GetListsAsync()
        {
            await CreateConnectionAsync();
            return await connection.Table<ToDoList>().ToListAsync();
        }

        public async Task UpdateListAsync(ToDoList list)
        {
            await CreateConnectionAsync();
            await connection.UpdateAsync(list);
            OnListUpdated?.Invoke(this, list);
        }
        #endregion
    }
}

