using System;
using MyToDoMauiApp.Repositories;

namespace MyToDoMauiApp.ViewModels
{
	// represents to-do list item used to create and/or edit items.
	public class ItemViewModel : ViewModelBase
	{
		private readonly ITodoItemRepository repository;

		public ItemViewModel(ITodoItemRepository repository)
		{
			this.repository = repository;
		}
	}
}

