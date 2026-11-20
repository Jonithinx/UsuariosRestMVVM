using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UsuariosRestMVVM.Models;
using UsuariosRestMVVM.Services;

namespace UsuariosRestMVVM.ViewModels;

public partial class CrearUsuarioViewModel : ObservableObject
{
    private readonly ApiService _apiService;

    [ObservableProperty]
    public partial string Name { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Email { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Phone { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Website { get; set; } = string.Empty;

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial string ResultadoMensaje { get; set; } = string.Empty;

    [ObservableProperty]
    public partial int? UsuarioCreadoId { get; set; }

    [ObservableProperty]
    public partial bool HasResultado { get; set; }

    [ObservableProperty]
    public partial string ErrorMessage { get; set; } = string.Empty;

    [ObservableProperty]
    public partial bool HasError { get; set; }

    public CrearUsuarioViewModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [RelayCommand]
    private async Task GuardarUsuarioAsync()
    {
        if (IsBusy)
            return;

        HasError = false;
        ErrorMessage = string.Empty;
        HasResultado = false;
        UsuarioCreadoId = null;
        ResultadoMensaje = string.Empty;

        if (string.IsNullOrWhiteSpace(Name) ||
            string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(Phone) ||
            string.IsNullOrWhiteSpace(Website))
        {
            ErrorMessage = "Por favor, completa todos los campos (Nombre, Correo, Teléfono y Sitio web).";
            HasError = true;
            return;
        }

        try
        {
            IsBusy = true;

            var nuevoUsuario = new Usuario
            {
                Name = Name.Trim(),
                Email = Email.Trim(),
                Phone = Phone.Trim(),
                Website = Website.Trim()
            };

            var usuarioCreado = await _apiService.CrearUsuarioAsync(nuevoUsuario);

            UsuarioCreadoId = usuarioCreado.Id;
            ResultadoMensaje = $"Usuario \"{usuarioCreado.Name}\" creado exitosamente.";
            HasResultado = true;

            Name = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Website = string.Empty;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"No se pudo crear el usuario. Verifica tu conexión e inténtalo de nuevo.\n({ex.Message})";
            HasError = true;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
