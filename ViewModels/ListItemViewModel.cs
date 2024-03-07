using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDoMauiApp.Models;

using MyToDoMauiApp.Repositories;
using MyToDoMauiApp.Views;

namespace MyToDoMauiApp.ViewModels
{
	public partial class ListItemViewModel : ViewModelBase
	{
        private readonly ITodoItemRepository repository;
        private readonly IServiceProvider services;

        [ObservableProperty]
        ToDoList listItem;

        public ListItemViewModel(ITodoItemRepository repository, IServiceProvider services)
		{
            this.repository = repository;
            this.services = services;
            ListItem = new ToDoList() { ListDateName = DateTime.Now.Date };
        }

        [RelayCommand]
        public async Task SaveAsync()
        {
            await repository.AddOrUpdateListAsync(ListItem);
            await Navigation.PopAsync();
        }

        [RelayCommand]
        public async Task AddItemAsync()
        {
            var itemView = services.GetRequiredService<ItemView>();
            var vm = itemView.BindingContext as ItemViewModel;

            vm.ListItem = listItem;
            itemView.Title = "Add new task to list";

            await Navigation.PushAsync(itemView);
        }
    }
}

