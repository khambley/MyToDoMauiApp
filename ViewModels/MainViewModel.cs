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

        [ObservableProperty] TodoItemViewModel? selectedItem;

        public MainViewModel(ITodoItemRepository repository, IServiceProvider services)
		{
            repository.OnItemAdded += (sender, item) => items?.Add(CreateTodoItemViewModel(item));
            repository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadDataAsync());

            this.repository = repository;
            this.services = services;
            Task.Run(async () => await LoadDataAsync());
        }

        partial void OnSelectedItemChanging(TodoItemViewModel? value)
        {
            if(value == null)
            {
                return;
            }

            MainThread.BeginInvokeOnMainThread(async () => await NavigateToItemAsync(value));
        }

        private async Task NavigateToItemAsync(TodoItemViewModel item)
        {
            var itemView = services.GetRequiredService<ItemView>();
            var vm = itemView.BindingContext as ItemViewModel;
            vm.Item = item.Item;
            itemView.Title = "Edit To-Do Item";

            await Navigation.PushAsync(itemView);
        }

        private async Task LoadDataAsync()
        {
            var items = await repository.GetItemsAsync();
            var itemViewModels = items.Select(i => CreateTodoItemViewModel(i));
            Items = new ObservableCollection<TodoItemViewModel>(itemViewModels);
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

