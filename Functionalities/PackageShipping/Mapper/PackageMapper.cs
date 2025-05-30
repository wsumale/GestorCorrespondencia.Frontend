using GestorCorrespondencia.Frontend.Functionalities.PackageShipping.DTO;
using GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Model;
using Riok.Mapperly.Abstractions;

namespace GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Mapper;

[Mapper]
public static partial class PackageMapper
{
    [MapProperty(nameof(ShippingForm.LocationId), nameof(PackageRequestDTO.RecipientLocation))]
    [MapProperty(nameof(ShippingForm.AddresseeId), nameof(PackageRequestDTO.Addressee))]
    [MapProperty(nameof(ShippingForm.Observations), nameof(PackageRequestDTO.Observations))]
    [MapProperty(nameof(ShippingForm.Details), nameof(PackageRequestDTO.PackageDetails))]
    internal static partial PackageRequestDTO ToPackageRequestDTO(this ShippingForm form);

    [MapProperty(nameof(ShippingDetail.TypeId), nameof(PackageDetailsRequestDTO.TypeItemId))]
    [MapProperty(nameof(ShippingDetail.Comment), nameof(PackageDetailsRequestDTO.Comments))]
    internal static partial PackageDetailsRequestDTO ToPackageDetailsRequestDTO(this ShippingDetail detail);
}
