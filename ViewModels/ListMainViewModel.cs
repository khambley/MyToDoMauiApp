using System;
using CommunityToolkit.Mvvm.Input;
using MyToDoMauiApp.Repositories;
using MyToDoMauiApp.Views;

namespace MyToDoMauiApp.ViewModels
{
	public partial class ListMainViewModel : ViewModelBase
	{
		private readonly ITodoItemRepository repository;
		private readonly IServiceProvider services;


        public ListMainViewModel(ITodoItemRepository repository, IServiceProvider services)
		{
			this.repository = repository;
			this.services = services;
			Task.Run(async () => await LoadDataAsync());
		}

		private async Task LoadDataAsync()
		{

		}

        [RelayCommand]
        public async Task AddListItemAsync() => await Navigation.PushAsync(services.GetRequiredService<ListItemView>());
    }
}

