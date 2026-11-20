using UsuariosRestMVVM.ViewModels;

namespace UsuariosRestMVVM.Views;

public partial class UsuariosPage : ContentPage
{
    private readonly UsuariosViewModel _viewModel;

    public UsuariosPage(UsuariosViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_viewModel.Usuarios.Count == 0 && !_viewModel.IsBusy)
        {
            await _viewModel.CargarUsuariosCommand.ExecuteAsync(null);
        }
    }
}
