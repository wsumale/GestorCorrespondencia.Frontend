using GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Components;
public partial class PackageDetailsForm
{
    [Parameter] public ShippingForm? shippingForm { get; set; }
    [Parameter] public EventCallback<ShippingForm> OnModelUpdated { get; set; }

    private Dictionary<int, string> types = new()
    {
        { 1, "Documentos" },
        { 2, "Paquete" },
        { 3, "Carta" },
        { 4, "Caja" }
    };

    private async Task AddDetailAsync()
    {
        shippingForm!.Details.Add(new ShippingDetail());
        await OnModelUpdated.InvokeAsync(shippingForm);
    }

    private async Task DeleteDetailAsync(ShippingDetail detalle)
    {
        shippingForm!.Details.Remove(detalle);
        await OnModelUpdated.InvokeAsync(shippingForm);
    }

    private async Task EnterAsync(KeyboardEventArgs e)
    {
        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await AddDetailAsync();
        }
    }

    private void OnTypeChanged(object? value, ShippingDetail detail)
    {
        if (value is int selectedId && types.TryGetValue(selectedId, out var selectedValue))
        {
            detail.Type = selectedValue;
        }
        else
        {
            detail.Type = null;
        }
    }


}