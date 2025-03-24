using DigesettAPP;
using DigesettAPP.Service;
using DigesettAPP.ViewModel;
using DigesettAPP.Views;
using Microsoft.Extensions.Logging;

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
                fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                fonts.AddFont("Poppins-Light.ttf", "PoppinsLight");
                fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
            });

        builder.Services.AddSingleton<Home>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<LoginViewModel>();

        //Agregar las demas paginas asi mismo aqui

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}