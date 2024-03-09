using MyToDoMauiApp.ViewModels;

namespace MyToDoMauiApp.Views;

public partial class ListItemView : ContentPage
{
	public ListItemView(ListItemViewModel viewModel)
	{
		InitializeComponent();
        viewModel.Navigation = Navigation;
        BindingContext = viewModel;

        // a fix for the item remaining selected when we go back,
        // couldn't select another row without this fix.
        ItemsListView.ItemSelected += (s, e) => ItemsListView.SelectedItem = null;
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
