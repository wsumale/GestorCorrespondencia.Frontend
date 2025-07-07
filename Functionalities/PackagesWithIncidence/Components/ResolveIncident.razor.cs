using GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.DTO;
using GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Http;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.SGL;
using GestorCorrespondencia.Frontend.Services.SGU;
using GestorCorrespondencia.Frontend.Shared.Model;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Components;
public partial class ResolveIncident
{
    [Parameter] public int IncidentId { get; set; }
    [Parameter] public int IncidentTypeId { get; set; }

    private int selectedIndex = 0;
}