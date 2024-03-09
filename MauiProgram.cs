using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Logging;
using MyToDoMauiApp.Repositories;

namespace MyToDoMauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .RegisterServices()
			.RegisterViewModels()
			.RegisterViews();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        //AppCenter.Start("ios=c493b77d-6140-4b5b-af79-cf29b09e6e5c;" +
        //          "android=697f52e7-992c-4125-b883-e9bcadc12772;",
        //          typeof(Analytics), typeof(Crashes));

        return builder.Build();
	}

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
	{
        mauiAppBuilder.Services.AddSingleton<ITodoItemRepository, TodoItemRepository>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<ViewModels.MainViewModel>();
        mauiAppBuilder.Services.AddTransient<ViewModels.ItemViewModel>();
        mauiAppBuilder.Services.AddTransient<ViewModels.ListMainViewModel>();
        mauiAppBuilder.Services.AddTransient<ViewModels.ListItemViewModel>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<Views.MainView>();
        mauiAppBuilder.Services.AddTransient<Views.ItemView>();
        mauiAppBuilder.Services.AddTransient<Views.ListMainView>();
        mauiAppBuilder.Services.AddTransient<Views.ListItemView>();
        return mauiAppBuilder;
    }
}

