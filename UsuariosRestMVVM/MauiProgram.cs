using Microsoft.Extensions.Logging;
using UsuariosRestMVVM.Services;
using UsuariosRestMVVM.ViewModels;
using UsuariosRestMVVM.Views;

namespace UsuariosRestMVVM
{
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
                });

            builder.Services.AddSingleton<ApiService>();

            builder.Services.AddTransient<UsuariosViewModel>();
            builder.Services.AddTransient<DetalleUsuarioViewModel>();
            builder.Services.AddTransient<CrearUsuarioViewModel>();

            builder.Services.AddTransient<UsuariosPage>();
            builder.Services.AddTransient<DetalleUsuarioPage>();
            builder.Services.AddTransient<CrearUsuarioPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
