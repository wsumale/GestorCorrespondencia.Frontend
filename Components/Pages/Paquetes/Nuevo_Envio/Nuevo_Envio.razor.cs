using Microsoft.AspNetCore.Components;
using GestorCorrespondencia.Frontend.Model.Paquetes.Nuevo_Envio;
using Radzen;
using Radzen.Blazor;

namespace GestorCorrespondencia.Frontend.Components.Pages.Paquetes.Nuevo_Envio;
public partial class Nuevo_Envio
{
    private string title = "Formulario de Env�o";
    private readonly FormularioEnvio modelo = new();

    protected override void OnInitialized()
    {
        LayoutService.ActualizarHeader("Enviar Paquete");
    }

    private async Task CanChange(StepsCanChangeEventArgs args)
    {

        changeTitle(args);

        string title_1 = "Informaci�n del env�o";
        string msg_1 = "Debe completar el formulario";

        string title_2 = "Detalle del env�o";
        string msg_2 = "Debe agregar al menos una l�nea de detalle con todos los campos completos.";
        string msg_3 = "Debe completar las l�neas de detalle.";

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
        title = args.NewIndex == 2 ? "Resumen del Env�o" : "Formulario de Env�o";
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

        var response = await DialogService.Confirm("�Est� seguro de que desea cancelar la env�o del paquete?", "Cancelar", new ConfirmOptions() { OkButtonText = "S�", CancelButtonText = "No" });

        if (response == true)
        {
            NavigationManager.NavigateTo("/");
        }
    }
}