using System.Text.Json;
using GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Model;
using GestorCorrespondencia.Frontend.Services.SGU;
using GestorCorrespondencia.Frontend.Shared.Model;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Components;
public partial class PackageDestinationForm
{
    [Inject] ILogger<PackageDestinationForm> _logger { get; set; } = default!;
    [Inject] SGUService SGUService { get; set; } = default!;

    [Parameter] public ShippingForm shippingForm { get; set; }
    [Parameter] public IList<Location> Locations { get; set; }
    [Parameter] public EventCallback<ShippingForm> OnModelUpdated { get; set; }
    [Parameter] public EventCallback<bool> OnLoading { get; set; }

    private IList<User> Users;

    protected override async void OnInitialized()
    {
        if(shippingForm.LocationId > 0)
        {
            await GetUsersByLocationId();
        }
    }

    private async Task UpdateModel()
    {
        await OnModelUpdated.InvokeAsync(shippingForm);
    }

    private async Task UpdateLoader(bool loading)
    {
        await OnLoading.InvokeAsync(loading);
    }

    private async Task UpdateDropdownValues()
    {
        var selectedLocation = Locations.FirstOrDefault(loc => loc.LocationId == shippingForm.LocationId);
        shippingForm.LocationName = selectedLocation?.LocationName!;
        await UpdateModel();
        await GetUsersByLocationId();
    }

    private async Task GetUsersByLocationId()
    {
        await UpdateLoader(true);
        Users = await SGUService.GetUsersByLocationAsync(shippingForm.LocationId);
        await UpdateLoader(false);
        StateHasChanged();
    }

    private async Task UpdateAddresseeInfo()
    {
        var selectedUser = Users.FirstOrDefault(u => u.Id == shippingForm.AddresseeId);
        shippingForm.AddresseeName = selectedUser!.Name;
        shippingForm.AddresseeEmail = selectedUser.Email;
        await UpdateModel();
    }
}