using System;
using MyToDoMauiApp.Views;
using MyToDoMauiApp.Repositories;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using MyToDoMauiApp.Models;

namespace MyToDoMauiApp.ViewModels
{
	public partial class MainViewModel : ViewModelBase
	{
        private readonly ITodoItemRepository repository;
        private readonly IServiceProvider services;

        [ObservableProperty]
        ObservableCollection<TodoItemViewModel>? items;

        public MainViewModel(ITodoItemRepository repository, IServiceProvider services)
		{
            repository.OnItemAdded += (sender, item) => items?.Add(CreateTodoItemViewModel(item));
            repository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadDataAsync());

            this.repository = repository;
            this.services = services;
            Task.Run(async () => await LoadDataAsync());
        }

        private async Task LoadDataAsync()
        {
            var items = await repository.GetItemsAsync();
            var itemViewModels = items.Select(i => CreateTodoItemViewModel(i));
        }

        private TodoItemViewModel CreateTodoItemViewModel(TodoItem item)
        {
            var itemViewModel = new TodoItemViewModel(item);
            itemViewModel.ItemStatusChanged += ItemStatusChanged;
            return itemViewModel;
        }

        private void ItemStatusChanged(object? sender, EventArgs e)
        {
        }

        [RelayCommand]
        public async Task AddItemAsync() => await Navigation.PushAsync(services.GetRequiredService<ItemView>());
    }
}

