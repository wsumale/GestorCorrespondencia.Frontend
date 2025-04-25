using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;
using Unisuper.GestorCorrespondencia.Frontend.Model;

public class SesionService
{
    private readonly IJSRuntime _js;
    private readonly NavigationManager _nav;
    private const string KEY = "usuario";

    public SesionService(IJSRuntime js, NavigationManager nav)
    {
        _js = js;
        _nav = nav;
    }

    public async Task<UsuarioSesion?> ObtenerSesion()
    {
        var json = await _js.InvokeAsync<string>("sessionStorage.getItem", KEY);
        if (string.IsNullOrWhiteSpace(json)) return null;
        return JsonSerializer.Deserialize<UsuarioSesion>(json);
    }

    public async Task ValidarSesionONavegarAsync()
    {
        string rutaRedireccion = "/";
        var sesion = await ObtenerSesion();
        if (sesion == null || string.IsNullOrEmpty(sesion.token))
        {
            _nav.NavigateTo(rutaRedireccion);
        }
    }

    public async Task GuardarSesion(UsuarioSesion usuario)
    {
        var json = JsonSerializer.Serialize(usuario);
        await _js.InvokeVoidAsync("sessionStorage.setItem", KEY, json);
    }

    public async Task CerrarSesion()
    {
        await _js.InvokeVoidAsync("sessionStorage.removeItem", KEY);
    }
}
