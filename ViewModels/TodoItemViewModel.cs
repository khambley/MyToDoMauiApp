using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MyToDoMauiApp.Models;
using CommunityToolkit.Mvvm.Input;

namespace MyToDoMauiApp.ViewModels
{
	// Represents each item in the to-do list
	public partial class TodoItemViewModel : ViewModelBase
	{
		public TodoItemViewModel(TodoItem item) => Item = item;

		public event EventHandler ItemStatusChanged;

		[ObservableProperty]
		TodoItem item;

		public string StatusText => Item.Completed ? "Reactivate" : "Completed";

		[RelayCommand]
		void ToggleCompleted()
		{
			Item.Completed = !Item.Completed;
			ItemStatusChanged?.Invoke(this, new EventArgs());
		}
	}
}

