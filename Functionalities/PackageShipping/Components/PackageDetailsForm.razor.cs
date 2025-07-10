using GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Components;
public partial class PackageDetailsForm
{
    [Parameter] public ShippingForm? shippingForm { get; set; }
    [Parameter] public IList<PackageDetailTypeItem>? TypeItems { get; set; }
    [Parameter] public EventCallback<ShippingForm> OnModelUpdated { get; set; }

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
        if (value is int selectedId && TypeItems != null)
        {
            var selectedItem = TypeItems.FirstOrDefault(t => t.Id == selectedId);
            detail.Type = selectedItem?.Description;
        }
        else
        {
            detail.Type = null;
        }
    }


}