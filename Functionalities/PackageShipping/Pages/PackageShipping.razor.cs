using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Model;
using GestorCorrespondencia.Frontend.Services.SGL;
using GestorCorrespondencia.Frontend.Shared.Model;
using GestorCorrespondencia.Frontend.Functionalities.PackageShipping.DTO;
using GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Mapper;
using System.Text.Json;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Services.Dialogs;

namespace GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Pages;
public partial class PackageShipping
{
    [Inject] DialogService DialogService { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    [Inject] SGLService SGLService { get; set; } = default!;
    [Inject] ApiPostService ApiPostService { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;

    private bool loading = true;
    private bool busy;
    private IList<Location>? Locations;

    protected override async Task OnInitializedAsync()
    {
        Locations = await SGLService.GetLocationsSendPackagesAsync();

        loading = false;
    }

    private string title = "Formulario de Envío";
    private ShippingForm shippingForm = new ShippingForm();

    private async Task CanChangeAsync(StepsCanChangeEventArgs args)
    {
        changeTitle(args);

        string title_1 = "Información del envío";
        string msg_1 = "Debe completar el formulario";

        string title_2 = "Detalle del envío";
        string msg_2 = "Debe agregar al menos una línea de detalle con todos los campos completos.";
        string msg_3 = "Debe completar las líneas de detalle.";

        // paso 1 a paso 2
        if (args.SelectedIndex == 0 && args.NewIndex == 1 && !shippingForm.HasValidDestination)
        {
            await DialogService.Alert(msg_1, title_1);
            args.PreventDefault();
        }

        // paso 2 a paso 3
        if (args.SelectedIndex == 1 && args.NewIndex == 2 && !shippingForm.HasValidDetails)
        {
            if (shippingForm.Details.Count() <= 1)
            {
                await DialogService.Alert(msg_2, title_2);
                args.PreventDefault();
            }
            if (shippingForm.Details.Count() > 1)
            {
                await DialogService.Alert(msg_3, title_2);
                args.PreventDefault();
            }
        }

        // paso 1 a paso 3
        if (args.SelectedIndex == 0 && args.NewIndex == 2)
        {
            bool show = false;

            if (!shippingForm.HasValidDestination && !show)
            {
                await DialogService.Alert(msg_1, title_1);
                args.PreventDefault();
                show = true;
            }

            if (!shippingForm.HasValidDetails && !show)
            {
                if (shippingForm.Details.Count() <= 1)
                {
                    await DialogService.Alert(msg_2, title_2);
                    args.PreventDefault();
                    show = true;
                }
                if (shippingForm.Details.Count() > 1)
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

    private async Task SubmitAsync()
    {
        try
        {
            loading = busy = true;

            PackageRequestDTO dto = shippingForm.ToPackageRequestDTO();
            var response = await ApiPostService.PostAsync("paquetes", dto, 1, true);

            if (response.IsSuccessStatusCode)
            {
                await SuccessAsync();
            } else
            {
                await CustomDialogService.OpenViewErrorsAsync(response);
            }
        } catch(Exception e)
        {
            await CustomDialogService.OpenInternalErrorAsync(e);
        } finally
        {
            loading = busy = false;
        }
    }

    private async Task SuccessAsync()
    {

        var redirect = await DialogService.Alert("Paquete creado con éxito", "Operación exitosa", new AlertOptions { CloseDialogOnEsc = false, CloseDialogOnOverlayClick = false, OkButtonText = "Aceptar" });
        if (redirect == true)
        {
            NavigationManager.NavigateTo("/paquetes/mis_paquetes");
        }
    }

    private async Task CancelAsync()
    {
        var response = await DialogService.Confirm("¿Está seguro de que desea cancelar el envío del paquete?", "Cancelar", new ConfirmOptions() { OkButtonText = "Sí", CancelButtonText = "No" });

        if (response == true)
        {
            NavigationManager.NavigateTo("/principal/home");
        }
    }
}