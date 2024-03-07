using MyToDoMauiApp.ViewModels;

namespace MyToDoMauiApp.Views;

public partial class ListMainView : ContentPage
{
	public ListMainView(ListMainViewModel viewModel)
	{
		InitializeComponent();
        viewModel.Navigation = Navigation;
        BindingContext = viewModel;

        ListItemsListView.ItemSelected += (s, e) => ListItemsListView.SelectedItem = null;
    }
}
