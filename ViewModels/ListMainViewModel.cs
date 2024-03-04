using System;
using MyToDoMauiApp.Repositories;

namespace MyToDoMauiApp.ViewModels
{
	public class ListMainViewModel : ViewModelBase
	{
		private readonly ITodoItemRepository repository;

		public ListMainViewModel(ITodoItemRepository repository)
		{
			this.repository = repository;
			Task.Run(async () => await LoadDataAsync());
		}

		private async Task LoadDataAsync()
		{

		}
	}
}

