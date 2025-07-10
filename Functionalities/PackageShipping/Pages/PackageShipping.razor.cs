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
using GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Http;

namespace GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Pages;
public partial class PackageShipping
{
    [Inject] DialogService DialogService { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    [Inject] SGLService SGLService { get; set; } = default!;
    [Inject] PackageShippingHttp PackageShippingHttp { get; set; } = default!;
    [Inject] ApiPostService ApiPostService { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;

    private bool loading = false;
    private bool busy;
    private IList<Location>? Locations;
    private IList<PackageDetailTypeItem>? TypeItems;

    protected override async Task OnInitializedAsync()
    {
        loading = true;
        Locations = await SGLService.GetLocationsSendPackagesAsync();
        TypeItems = await PackageShippingHttp.GetPackageDetailTypeItemsAsync();
        loading = false;
        StateHasChanged();
    }

    private string title = "Formulario de Env�o";
    private ShippingForm shippingForm = new ShippingForm();

    private async Task CanChangeAsync(StepsCanChangeEventArgs args)
    {
        changeTitle(args);

        string title_1 = "Informaci�n del env�o";
        string msg_1 = "Debe completar el formulario";

        string title_2 = "Detalle del env�o";
        string msg_2 = "Debe agregar al menos una l�nea de detalle con todos los campos completos.";
        string msg_3 = "Debe completar las l�neas de detalle.";

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
        title = args.NewIndex == 2 ? "Resumen del Env�o" : "Formulario de Env�o";
    }

    private void OnChange()
    {
        Console.WriteLine("Change");
    }

    private async Task SubmitAsync()
    {
        PackageRequestDTO dto = shippingForm.ToPackageRequestDTO();
        loading = busy = true;
        PackageResponseDTO response = await PackageShippingHttp.SendPackagedAsync(dto);
        await SuccessAsync(response);
        loading = busy = false;
    }

    private async Task SuccessAsync(PackageResponseDTO response)
    {

        var redirect = await DialogService.Alert($"Paquete <strong>{response.PackageId}</strong> creado con �xito", "Operaci�n exitosa", new AlertOptions { CloseDialogOnEsc = false, CloseDialogOnOverlayClick = false, OkButtonText = "Aceptar" });
        if (redirect == true)
        {
            NavigationManager.NavigateTo($"/paquetes/rastrear/{response.PackageId}");
        }
    }

    private async Task CancelAsync()
    {
        var response = await DialogService.Confirm("�Est� seguro de que desea cancelar el env�o del paquete?", "Cancelar", new ConfirmOptions() { OkButtonText = "S�", CancelButtonText = "No" });

        if (response == true)
        {
            NavigationManager.NavigateTo("/principal/home");
        }
    }
}