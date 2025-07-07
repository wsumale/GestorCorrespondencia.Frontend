using GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Http;
using GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Model;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Components;
public partial class IncidentDetail
{
    [Inject] PackagesWithIncidentHttp PackagesWithIncidentHttp { get; set; } = default!;
    [Parameter] public int IncidentId { get; set; }

    private bool loading = false;
    private bool showContent = false;

    private Incident incident = new();

    protected override async Task OnInitializedAsync()
    {
        loading = true;

        await LoadDataAsync();
        base.OnInitialized();

        loading = false;
        StateHasChanged();
    }

    private async Task LoadDataAsync()
    {
        incident = await PackagesWithIncidentHttp.GetIncidentDetailAsync(IncidentId);
        showContent = incident.IncidentId > 0 ? true : false;
    }
}