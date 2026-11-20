using UsuariosRestMVVM.ViewModels;

namespace UsuariosRestMVVM.Views;

public partial class CrearUsuarioPage : ContentPage
{
    public CrearUsuarioPage(CrearUsuarioViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
