using GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Model;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Components;
public partial class PackageSummary
{
    [Parameter] public ShippingForm shippingForm { get; set; }
}