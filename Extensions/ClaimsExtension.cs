using GestorCorrespondencia.Frontend.Functionalities.Authentication.Model.Session;
using System.Security.Claims;
using System.Text.Json;

namespace GestorCorrespondencia.Frontend.Extensions;

public static class ClaimsExtensions
{
    public static SessionUser ReconstructUserFromClaims(this ClaimsPrincipal user)
    {
        var usuario = new SessionUser
        {
            EmployeeCode = user.FindFirst("EmployeeCode")?.Value ?? string.Empty,
            Name = user.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
            User = user.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty,
            Rol = user.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
            LocationId = int.TryParse(user.FindFirst("LocationId")?.Value, out var locId) ? locId : 0,
            StoreId = int.TryParse(user.FindFirst("StoreId")?.Value, out var storeId) ? storeId : 0,
            Store = user.FindFirst("Store")?.Value ?? string.Empty,
            Menu = user.Claims
                .Where(c => c.Type.StartsWith("Menu:"))
                .Select(c =>
                {
                    try
                    {
                        return JsonSerializer.Deserialize<Menu>(c.Value);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(m => m != null)
                .ToList()!
        };

        return usuario;
    }
}
