using MyToDoMauiApp.ViewModels;

namespace MyToDoMauiApp.Views;

public partial class MainView : ContentPage
{
	// displays the list of to-dos
	public MainView(MainViewModel viewModel)
	{
		InitializeComponent();
		viewModel.Navigation = Navigation;
		BindingContext = viewModel;

		ItemsListView.ItemSelected += (s, e) => ItemsListView.SelectedItem = null;
	}
}
