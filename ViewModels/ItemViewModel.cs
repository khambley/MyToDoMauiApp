using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDoMauiApp.Models;
using MyToDoMauiApp.Repositories;

namespace MyToDoMauiApp.ViewModels
{
	// represents to-do list item used to create and/or edit items.
	public partial class ItemViewModel : ViewModelBase
	{
		private readonly ITodoItemRepository repository;

		// replaces Fody weaver from Xamarin
		[ObservableProperty]
		TodoItem item;

		public ItemViewModel(ITodoItemRepository repository)
		{
			this.repository = repository;
			Item = new TodoItem()
			{
				DueDate = DateTime.Now.AddDays(1)
			};
		}

		[RelayCommand]
		public async Task SaveAsync()
		{
			await repository.AddOrUpdateAsync(Item);
			await Navigation.PopAsync();
		}

		[RelayCommand]
		public async Task DeleteItemAsync()
		{
			bool answer = await Application.Current.MainPage.DisplayAlert("Delete To-do Item", "Are you sure you want to delete this item? This cannot be undone.", "Yes", "Cancel");
			if (answer)
			{
                await repository.DeleteItemAsync(Item);
                await Navigation.PopAsync();
            }
			else
			{
                await Application.Current.MainPage.DisplayAlert("Message", "The item was not deleted.", "OK");
            }
		}
	}
}

