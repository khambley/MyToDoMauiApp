﻿using System;
using System.Collections.ObjectModel;
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

        [ObservableProperty]
        ObservableCollection<TodoItemViewModel>? items;

        [ObservableProperty]
        bool showAll;

        [ObservableProperty]
        TodoItemViewModel? selectedItem;

        [RelayCommand]
        private async Task ToggleFilterAsync()
        {
            ShowAll = !ShowAll;
            await LoadDataAsync();
        }

        public ListItemViewModel(ITodoItemRepository repository, IServiceProvider services)
		{
            repository.OnItemAdded += (sender, item) => items?.Add(CreateTodoItemViewModel(item));
            repository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadDataAsync());
            repository.OnItemDeleted += (sender, item) => Task.Run(async () => await LoadDataAsync());

            this.repository = repository;
            this.services = services;
            ListItem = ListItem ?? new ToDoList() { ListDateName = DateTime.Now.Date };
            Task.Run(async () => await LoadDataAsync());
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

            
            if(listItem.ListId == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "You must save the list before adding tasks to it", "Ok");

            }
            else
            {
                vm.ListItem = listItem;

                itemView.Title = "Add new task to list";

                await Navigation.PushAsync(itemView);
            }
            
        }

        public async Task LoadDataAsync()
        {
            var items = await repository.GetItemsAsync();

            // Only show items on the current list for that day
            items = items.Where(i => i.ListId == ListItem.ListId).ToList();

            //Show only active (not completed) items
            if (!ShowAll)
            {
                items = items.Where(x => x.Completed == false).ToList();
            }

            var itemViewModels = items.Select(i => CreateTodoItemViewModel(i));

            Items = new ObservableCollection<TodoItemViewModel>(itemViewModels);
        }

        private TodoItemViewModel CreateTodoItemViewModel(TodoItem item)
        {
            var itemViewModel = new TodoItemViewModel(repository, services, item);
            itemViewModel.ItemStatusChanged += ItemStatusChanged;
            return itemViewModel;
        }

        private void ItemStatusChanged(object? sender, EventArgs e)
        {
            if (sender is TodoItemViewModel item)
            {
                if (!ShowAll && item.Item.Completed)
                {
                    Items?.Remove(item);
                }

                Task.Run(async () => await repository.UpdateItemAsync(item.Item));

            }
        }

        partial void OnSelectedItemChanging(TodoItemViewModel? value)
        {
            if (value == null)
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
    }
}

