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

    protected override void OnAppearing()
    {
        if (this.Title == "Edit Task List")
        {
            listDatePicker.IsEnabled = false;
            listDatePicker.IsVisible = false;
            listDateLabel.IsVisible = true;
            saveButton.IsEnabled = false;
            saveButton.Text = "";
        }
        base.OnAppearing();
    }

}
