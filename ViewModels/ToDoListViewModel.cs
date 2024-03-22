using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDoMauiApp.Models;
using MyToDoMauiApp.Repositories;

namespace MyToDoMauiApp.ViewModels
{
	public partial class ToDoListViewModel : ViewModelBase
	{
        private readonly ITodoItemRepository repository;
        [ObservableProperty]
		ToDoList listItem;

		public ToDoListViewModel(ITodoItemRepository repository, ToDoList list)
		{
            this.repository = repository;
            ListItem = list;
		}

		[RelayCommand]
        async Task DeleteListAsync()
		{
            bool answer = await Application.Current.MainPage.DisplayAlert("Delete Task List", "Are you sure you want to delete this item? This cannot be undone.", "Yes", "Cancel");
			if (answer)
			{
				var items = await repository.GetItemsByListId(ListItem.ListId);
				if(items.Count > 0)
				{
                    await Application.Current.MainPage.DisplayAlert("Task Items Found", "You must delete all items in the list in order to delete the list", "OK");
                }
				else
				{
					await repository.DeleteListAsync(ListItem);
				}
			}
            else
            {
                await Application.Current.MainPage.DisplayAlert("Message", "The item was not deleted.", "OK");
            }
        }
	}
}

