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

    protected override async void OnInitialized()
    {
        loading = true;

        await LoadDataAsync();
        base.OnInitialized();

        loading = false;
        StateHasChanged();
    }

    private async Task LoadDataAsync()
    {
        incident = await PackagesWithIncidentHttp.GetIncidentDetail(IncidentId);
        showContent = incident.IncidentId > 0 ? true : false;
    }
}