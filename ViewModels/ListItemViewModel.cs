using System;
using MyToDoMauiApp.Repositories;

namespace MyToDoMauiApp.ViewModels
{
	public class ListItemViewModel :ViewModelBase
	{
        private readonly ITodoItemRepository repository;

        public ListItemViewModel(ITodoItemRepository repository)
		{
            this.repository = repository;
        }
	}
}

