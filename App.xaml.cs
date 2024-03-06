namespace MyToDoMauiApp;

public partial class App : Application
{
	public App(Views.ListMainView view)
	{
		InitializeComponent();

		MainPage = new NavigationPage(view);
	}
}

