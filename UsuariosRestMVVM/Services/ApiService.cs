using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using UsuariosRestMVVM.Models;

namespace UsuariosRestMVVM.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
        };

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<List<Usuario>> GetUsuariosAsync()
    {
        using var response = await _httpClient.GetAsync("users");

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Error al obtener los usuarios. Código HTTP: {(int)response.StatusCode} ({response.ReasonPhrase}).");
        }

        var usuarios = await response.Content.ReadFromJsonAsync<List<Usuario>>(_jsonOptions);
        return usuarios ?? [];
    }

    public async Task<Usuario> GetUsuarioByIdAsync(int id)
    {
        using var response = await _httpClient.GetAsync($"users/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Error al obtener el usuario con ID {id}. Código HTTP: {(int)response.StatusCode} ({response.ReasonPhrase}).");
        }

        var usuario = await response.Content.ReadFromJsonAsync<Usuario>(_jsonOptions);
        return usuario ?? throw new InvalidOperationException(
            $"La respuesta de la API no contenía datos válidos para el usuario {id}.");
    }

    public async Task<Usuario> CrearUsuarioAsync(Usuario nuevoUsuario)
    {
        ArgumentNullException.ThrowIfNull(nuevoUsuario);

        var json = JsonSerializer.Serialize(nuevoUsuario, _jsonOptions);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        using var response = await _httpClient.PostAsync("users", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Error al crear el usuario. Código HTTP: {(int)response.StatusCode} ({response.ReasonPhrase}).");
        }

        var usuarioCreado = await response.Content.ReadFromJsonAsync<Usuario>(_jsonOptions);
        return usuarioCreado ?? throw new InvalidOperationException(
            "La API no devolvió el usuario creado.");
    }
}
