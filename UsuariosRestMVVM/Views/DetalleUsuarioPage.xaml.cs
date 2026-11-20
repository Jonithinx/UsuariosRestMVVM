using UsuariosRestMVVM.ViewModels;

namespace UsuariosRestMVVM.Views;

public partial class DetalleUsuarioPage : ContentPage
{
    public DetalleUsuarioPage(DetalleUsuarioViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
