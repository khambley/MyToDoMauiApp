using System;
using SQLite;

namespace MyToDoMauiApp.Models
{
	public class ToDoList
	{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime DateName { get; set; }
        public List<TodoItem>? TodoItems { get; set; }
    }
}

