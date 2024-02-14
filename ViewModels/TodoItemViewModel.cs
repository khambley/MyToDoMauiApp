﻿using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MyToDoMauiApp.Models;

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


	}
}

