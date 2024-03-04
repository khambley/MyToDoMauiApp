using MyToDoMauiApp.ViewModels;

namespace MyToDoMauiApp.Views;

public partial class ListItemView : ContentPage
{
	public ListItemView(ListItemViewModel viewModel)
	{
		InitializeComponent();
        viewModel.Navigation = Navigation;
        BindingContext = viewModel;
    }
}
