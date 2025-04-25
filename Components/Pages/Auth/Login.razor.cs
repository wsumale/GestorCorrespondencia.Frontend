using Microsoft.AspNetCore.Components;
using Radzen;
using GestorCorrespondencia.Frontend.Model.Auth;

namespace GestorCorrespondencia.Frontend.Components.Pages.Auth;
public partial class Login
{
    [Inject] protected NotificationService NotificationService { get; set; } = default!;

    private readonly FormularioLogin usuario = new();

    private async Task OnSubmit()
    {
        Console.WriteLine("Submit");
    }
}