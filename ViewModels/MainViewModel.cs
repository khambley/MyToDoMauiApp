using System;
using MyToDoMauiApp.Repositories;

namespace MyToDoMauiApp.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
        private readonly ITodoItemRepository repository;

        public MainViewModel(ITodoItemRepository repository)
		{
            this.repository = repository;
            Task.Run(async () => await LoadDataAsync());
        }

        private async Task LoadDataAsync()
        {
        }
    }
}

