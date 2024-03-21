using System;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyToDoMauiApp.Models;
using MyToDoMauiApp.Repositories;
using MyToDoMauiApp.Views;

namespace MyToDoMauiApp.ViewModels
{
	// represents to-do list item used to create and/or edit items.
	public partial class ItemViewModel : ViewModelBase
	{
		private readonly ITodoItemRepository repository;
        private readonly IServiceProvider services;

        // replaces Fody weaver from Xamarin
        [ObservableProperty]
		TodoItem item;

		[ObservableProperty]
		ToDoList listItem;

		public ItemViewModel(ITodoItemRepository repository, IServiceProvider services)
		{
			this.repository = repository;
            this.services = services;
            Item = new TodoItem()
			{
				TaskDate = DateTime.Now.Date
			};
		}

		[RelayCommand]
		public async Task SaveAsync()
		{
			if(ListItem != null)
			{
				Item.ListId = ListItem.ListId;
				Item.ListDateName = ListItem.ListDateName.Date;
			}

			if(Item.TaskDate != Item.ListDateName)
			{
				var lists = await repository.GetListsAsync();

				//Find a list with matching listdatename
				var found = lists.Any(d => d.ListDateName == Item.TaskDate);

				// If not found, create a new listItem with that date
				if (!found)
				{
					var newList = new ToDoList
					{
						ListDateName = Item.TaskDate
					};
					await repository.AddListAsync(newList);

					var newlists = await repository.GetListsAsync();

					var savedList = newlists.Where(date => date.ListDateName == Item.TaskDate).FirstOrDefault();

					if(savedList != null)
					{
                        Item.ListId = savedList.ListId;
						Item.ListDateName = savedList.ListDateName.Date;
                    }
					else
					{
                        await Application.Current.MainPage.DisplayAlert("Error", "A list for that date was not found", "OK");
                    }
				}
				// If found, change ListId and ListDateName to found list
				else
				{
                    var matchinglistDate = lists.Where(d => d.ListDateName == Item.TaskDate).FirstOrDefault();
                    if (matchinglistDate != null)
                    {
						Item.ListId = matchinglistDate.ListId;
						Item.ListDateName = matchinglistDate.ListDateName.Date;
                    }
					else
					{
                        await Application.Current.MainPage.DisplayAlert("Error", "A list for that date was not found", "OK");
                    }
                }	
			}

			await repository.AddOrUpdateAsync(Item);

			MainThread.BeginInvokeOnMainThread(async () => await NavigateToTaskListsAsync());
			//await Navigation.PopAsync();
		}

        private async Task NavigateToTaskListsAsync()
        {
			var listMainView = services.GetRequiredService<ListMainView>();
			var vm = listMainView.BindingContext as ListMainViewModel;

			await vm.LoadDataAsync();

			await Navigation.PushAsync(listMainView);

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

