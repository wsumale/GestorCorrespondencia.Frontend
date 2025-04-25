using Microsoft.AspNetCore.Components;
using GestorCorrespondencia.Frontend.Model.Paquetes.Nuevo_Envio;
using Radzen;
using Radzen.Blazor;

namespace GestorCorrespondencia.Frontend.Components.Pages.Paquetes.Nuevo_Envio;
public partial class Nuevo_Envio
{
    private string title = "Formulario de Envío";
    private readonly FormularioEnvio modelo = new();

    protected override void OnInitialized()
    {
        LayoutService.ActualizarHeader("Enviar Paquete");
    }

    private async Task CanChange(StepsCanChangeEventArgs args)
    {

        changeTitle(args);

        string title_1 = "Información del envío";
        string msg_1 = "Debe completar el formulario";

        string title_2 = "Detalle del envío";
        string msg_2 = "Debe agregar al menos una línea de detalle con todos los campos completos.";
        string msg_3 = "Debe completar las líneas de detalle.";

        // paso 1 a paso 2
        if (args.SelectedIndex == 0 && args.NewIndex == 1 && !modelo.PuedeEnviar)
        {
            await DialogService.Alert(msg_1, title_1);
            args.PreventDefault();
        }

        // paso 2 a paso 3
        if (args.SelectedIndex == 1 && args.NewIndex == 2 && !TieneDetallesValidos())
        {
            if (modelo.Detalles.Count() <= 1)
            {
                await DialogService.Alert(msg_2, title_2);
                args.PreventDefault();
            }
            if (modelo.Detalles.Count() > 1)
            {
                await DialogService.Alert(msg_3, title_2);
                args.PreventDefault();
            }
        }

        // paso 1 a paso 3
        if (args.SelectedIndex == 0 && args.NewIndex == 2)
        {
            bool show = false;

            if (!modelo.PuedeEnviar && !show)
            {
                await DialogService.Alert(msg_1, title_1);
                args.PreventDefault();
                show = true;
            }

            if (!TieneDetallesValidos() && !show)
            {
                if(modelo.Detalles.Count() <= 1)
                {
                    await DialogService.Alert(msg_2, title_2);
                    args.PreventDefault();
                    show = true;
                }
                if(modelo.Detalles.Count() > 1)
                {
                    await DialogService.Alert(msg_3, title_2);
                    args.PreventDefault();
                    show = true;
                }
            }
        }
    }

    private void changeTitle(StepsCanChangeEventArgs args)
    {
        title = args.NewIndex == 2 ? "Resumen del Envío" : "Formulario de Envío";
    }


    private void OnChange()
    {
        Console.WriteLine("Change");
    }

    private bool TieneDetallesValidos()
    {
        return modelo.Detalles != null &&
                modelo.Detalles.Any() &&
                modelo.Detalles.All(d =>
                    !string.IsNullOrEmpty(d.Tipo) &&
                    !string.IsNullOrEmpty(d.Comentarios) &&
                    d.Cantidad > 0);
    }

    private async Task Cancelar()
    {

        var response = await DialogService.Confirm("¿Está seguro de que desea cancelar la envío del paquete?", "Cancelar", new ConfirmOptions() { OkButtonText = "Sí", CancelButtonText = "No" });

        if (response == true)
        {
            NavigationManager.NavigateTo("/");
        }
    }
}