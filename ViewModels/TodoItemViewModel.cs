using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MyToDoMauiApp.Models;
using CommunityToolkit.Mvvm.Input;
using MyToDoMauiApp.Repositories;
using MyToDoMauiApp.Views;

namespace MyToDoMauiApp.ViewModels
{
	// Represents each item in the to-do list
	public partial class TodoItemViewModel : ViewModelBase
	{
        private readonly ITodoItemRepository repository;
        private readonly IServiceProvider services;

        public TodoItemViewModel(ITodoItemRepository repository, IServiceProvider services, TodoItem item)
		{
            this.repository = repository;
            this.services = services;
            Item = item;
        } 

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

