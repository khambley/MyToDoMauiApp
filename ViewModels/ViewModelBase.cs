using System;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MyToDoMauiApp.ViewModels
{
    [ObservableObject]
	public abstract partial class ViewModelBase
	{
        public INavigation Navigation { get; set; }
    }
}

