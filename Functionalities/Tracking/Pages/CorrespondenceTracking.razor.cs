using System;
using GestorCorrespondencia.Frontend.Functionalities.Tracking.Http;
using GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GestorCorrespondencia.Frontend.Functionalities.Tracking.Pages;
public partial class CorrespondenceTracking
{
    [Inject] TrackingHttp TrackingHttp { get; set; } = default!;
    [Inject] IJSRuntime JS { get; set; } = default!;

    [Parameter] public int? PackageIdParam { get; set; }

    private bool loading = false;

    private PackageTrackingForm form = new();
    private int? PackageId;
    private Package package = new();
    private bool foundPackage;
    private bool firstSearch = false;

    private string buffer = "";
    private string LastScanned = "";

    protected override async Task OnInitializedAsync()
    {
        if (PackageIdParam > 0)
        {
            form.PackageId = PackageIdParam;
            await SearchPackageAsync();
        }
    }

    private async Task SearchPackageAsync()
    {
        loading = true;
        package = await TrackingHttp.GetPackageAsync(form.PackageId ?? 0, false);
        foundPackage = package != null && package.PackageId > 0;
        firstSearch = true;
        loading = false;
        StateHasChanged();
    }

    private List<(string Texto, string Icon)> timelineItems = new()
    {
        ("Enviado a Correspondencia", "local_shipping"),
        ("Recibido por Correspondencia", "mail"),
        ("Enviado a Destino", "send"),
        ("En Recepción", "location_on"),
        ("Recibido", "check_circle")
    };

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JS.InvokeVoidAsync("startGlobalScanner", DotNetObjectReference.Create(this));
        }
    }

    [JSInvokable]
    public async Task OnScannerInputAsync(string code)
    {
        LastScanned = code;
        buffer = "";
        await ProcessScannedCodeAsync(code);
        StateHasChanged();
    }

    private async Task ProcessScannedCodeAsync(string code)
    {
        if (int.TryParse(code, out var packageId))
        {
            form.PackageId = packageId;
            await SearchPackageAsync();
        }
    }

}