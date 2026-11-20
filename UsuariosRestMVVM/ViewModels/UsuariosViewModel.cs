using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UsuariosRestMVVM.Models;
using UsuariosRestMVVM.Services;
using UsuariosRestMVVM.Views;

namespace UsuariosRestMVVM.ViewModels;

public partial class UsuariosViewModel : ObservableObject
{
    private readonly ApiService _apiService;

    public ObservableCollection<Usuario> Usuarios { get; } = [];

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial bool IsRefreshing { get; set; }

    [ObservableProperty]
    public partial string ErrorMessage { get; set; } = string.Empty;

    [ObservableProperty]
    public partial bool HasError { get; set; }

    public UsuariosViewModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [RelayCommand]
    private async Task CargarUsuariosAsync()
    {
        if (IsBusy)
            return;

        try
        {
            if (!IsRefreshing)
                IsBusy = true;

            HasError = false;
            ErrorMessage = string.Empty;

            var lista = await _apiService.GetUsuariosAsync();

            Usuarios.Clear();
            foreach (var usuario in lista)
            {
                Usuarios.Add(usuario);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"No se pudieron cargar los usuarios. Verifica tu conexión a internet.\n({ex.Message})";
            HasError = true;

            if (Shell.Current is not null)
            {
                await Shell.Current.DisplayAlertAsync("Error de red", ErrorMessage, "Aceptar");
            }
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async Task IrADetalleAsync(Usuario? usuario)
    {
        if (usuario is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(DetalleUsuarioPage)}?id={usuario.Id}");
    }

    [RelayCommand]
    private async Task IrACrearAsync()
    {
        await Shell.Current.GoToAsync(nameof(CrearUsuarioPage));
    }
}
