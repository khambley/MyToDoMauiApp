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

        private async Task CreateConnectionAsync()
        {
            if(connection != null)
            {
                return;
            }
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var databasePath = Path.Combine(documentPath, "TodoItems.db");

            connection = new SQLiteAsyncConnection(databasePath);

            await connection.CreateTableAsync<TodoItem>();
        }

        public async Task AddItemAsync(TodoItem item)
        {
            throw new NotImplementedException();
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
            return null; // Just to make it build
        }

        public async Task UpdateItemAsync(TodoItem item)
        {
            throw new NotImplementedException();
        }
    }
}

