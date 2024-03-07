using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDoMauiApp.Models;
using MyToDoMauiApp.Repositories;
using MyToDoMauiApp.Views;

namespace MyToDoMauiApp.ViewModels
{
	public partial class ListMainViewModel : ViewModelBase
	{
		private readonly ITodoItemRepository repository;
		private readonly IServiceProvider services;

        [ObservableProperty]
        ObservableCollection<ToDoListViewModel>? listItems;

        [ObservableProperty]
        ToDoListViewModel? selectedItem;

        public ListMainViewModel(ITodoItemRepository repository, IServiceProvider services)
		{
            repository.OnListAdded += (sender, item) => listItems?.Add(CreateToDoListViewModel(item));
            repository.OnListUpdated += (sender, item) => Task.Run(async () => await LoadDataAsync());
			repository.OnListDeleted += (sender, item) => Task.Run(async () => await LoadDataAsync());

            this.repository = repository;
			this.services = services;
			Task.Run(async () => await LoadDataAsync());
		}

		private async Task LoadDataAsync()
		{
			var todolists = await repository.GetListsAsync();
			var sortedList = todolists.OrderByDescending(d => d.ListDateName).ToList();
			var listItemViewModels = sortedList.Select(li => CreateToDoListViewModel(li));
			ListItems = new ObservableCollection<ToDoListViewModel>(listItemViewModels);
		}

		private ToDoListViewModel CreateToDoListViewModel(ToDoList todolist)
		{
			var listItemViewModel = new ToDoListViewModel(todolist);
			return listItemViewModel;
		}

        [RelayCommand]
        public async Task AddListItemAsync() => await Navigation.PushAsync(services.GetRequiredService<ListItemView>());

        partial void OnSelectedItemChanging(ToDoListViewModel? value)
        {
            if(value == null)
			{
				return;
			}

			MainThread.BeginInvokeOnMainThread(async () => await NavigateToListItemAsync(value));
        }

		private async Task NavigateToListItemAsync(ToDoListViewModel listItem)
		{
			var listItemView = services.GetRequiredService<ListItemView>();
			var vm = listItemView.BindingContext as ListItemViewModel;

			vm.ListItem = listItem.ListItem;

			await vm.LoadDataAsync();

			listItemView.Title = "Edit Task List";

			await Navigation.PushAsync(listItemView);
		}
    }
}

