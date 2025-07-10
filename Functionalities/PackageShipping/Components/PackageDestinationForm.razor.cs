using System.Text.Json;
using GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Model;
using GestorCorrespondencia.Frontend.Services.SGU;
using GestorCorrespondencia.Frontend.Shared.Model;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Components;
public partial class PackageDestinationForm
{
    [Inject] SGUService SGUService { get; set; } = default!;

    [Parameter] public ShippingForm? shippingForm { get; set; }
    [Parameter] public IList<Location>? Locations { get; set; }
    [Parameter] public EventCallback<ShippingForm> OnModelUpdated { get; set; }
    [Parameter] public EventCallback<bool> OnLoading { get; set; }

    private IList<User>? Users;

    protected override async Task OnInitializedAsync()
    {
        if(shippingForm!.LocationId > 0)
        {
            await GetUsersByLocationIdAsync();
        }
    }

    private async Task UpdateModelAsync()
    {
        await OnModelUpdated.InvokeAsync(shippingForm);
    }

    private async Task UpdateLoaderAsync(bool loading)
    {
        await OnLoading.InvokeAsync(loading);
    }

    private async Task UpdateDropdownValuesAsync()
    {
        var selectedLocation = Locations.FirstOrDefault(loc => loc.LocationId == shippingForm.LocationId);
        shippingForm.LocationName = selectedLocation?.LocationName!;
        await UpdateModelAsync();
        await GetUsersByLocationIdAsync();
    }

    private async Task GetUsersByLocationIdAsync()
    {
        await UpdateLoaderAsync(true);
        Users = await SGUService.GetUsersByLocationAsync(shippingForm!.LocationId, true);
        await UpdateLoaderAsync(false);
        StateHasChanged();
    }

    private async Task UpdateAddresseeInfoAsync()
    {
        var selectedUser = Users!.FirstOrDefault(u => u.Id == shippingForm.AddresseeId);
        shippingForm!.AddresseeName = selectedUser!.Name;
        shippingForm.AddresseeEmail = selectedUser.Email;
        await UpdateModelAsync();
    }
}