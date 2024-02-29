using System;
using MyToDoMauiApp.Views;

using MyToDoMauiApp.Repositories;
using CommunityToolkit.Mvvm.Input;

namespace MyToDoMauiApp.ViewModels
{
	public partial class MainViewModel : ViewModelBase
	{
        private readonly ITodoItemRepository repository;
        private readonly IServiceProvider services;

        public MainViewModel(ITodoItemRepository repository, IServiceProvider services)
		{
            this.repository = repository;
            this.services = services;
            Task.Run(async () => await LoadDataAsync());
        }

        private async Task LoadDataAsync()
        {
        }

        [RelayCommand]
        public async Task AddItemAsync() => await Navigation.PushAsync(services.GetRequiredService<ItemView>());
    }
}

