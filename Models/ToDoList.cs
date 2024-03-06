using System;
using SQLite;

namespace MyToDoMauiApp.Models
{
	public class ToDoList
	{
        [PrimaryKey, AutoIncrement]
        public int ListId { get; set; }
        public DateTime ListDateName { get; set; }
        //public List<TodoItem>? TodoItems { get; set; }
    }
}

