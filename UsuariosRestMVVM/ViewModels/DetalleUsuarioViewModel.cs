using CommunityToolkit.Mvvm.ComponentModel;
using UsuariosRestMVVM.Models;
using UsuariosRestMVVM.Services;

namespace UsuariosRestMVVM.ViewModels;

public partial class DetalleUsuarioViewModel : ObservableObject, IQueryAttributable
{
    private readonly ApiService _apiService;

    [ObservableProperty]
    public partial Usuario? Usuario { get; set; }

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial string ErrorMessage { get; set; } = string.Empty;

    [ObservableProperty]
    public partial bool HasError { get; set; }

    [ObservableProperty]
    public partial bool HasUsuario { get; set; }

    public DetalleUsuarioViewModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var rawId) && int.TryParse(rawId?.ToString(), out var id))
        {
            _ = CargarUsuarioAsync(id);
        }
        else
        {
            ErrorMessage = "No se recibió un identificador de usuario válido.";
            HasError = true;
            HasUsuario = false;
        }
    }

    private async Task CargarUsuarioAsync(int id)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            HasError = false;
            ErrorMessage = string.Empty;
            HasUsuario = false;

            Usuario = await _apiService.GetUsuarioByIdAsync(id);
            HasUsuario = true;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"No se pudo obtener el detalle del usuario.\n({ex.Message})";
            HasError = true;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
