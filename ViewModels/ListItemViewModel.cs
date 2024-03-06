using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDoMauiApp.Models;

using MyToDoMauiApp.Repositories;

namespace MyToDoMauiApp.ViewModels
{
	public partial class ListItemViewModel : ViewModelBase
	{
        private readonly ITodoItemRepository repository;

        [ObservableProperty]
        ToDoList listItem;

        public ListItemViewModel(ITodoItemRepository repository)
		{
            this.repository = repository;
            ListItem = new ToDoList() { DateName = DateTime.Now };
        }

        [RelayCommand]
        public async Task SaveAsync()
        {
            await repository.AddOrUpdateListAsync(ListItem);
            await Navigation.PopAsync();
        }
    }
}

