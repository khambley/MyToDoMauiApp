using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MyToDoMauiApp.Models;

namespace MyToDoMauiApp.ViewModels
{
	public partial class ToDoListViewModel : ViewModelBase
	{
		[ObservableProperty]
		ToDoList listItem;

		public ToDoListViewModel(ToDoList list)
		{
			ListItem = list;
		}
	}
}

