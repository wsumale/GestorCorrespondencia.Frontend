using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Model;
public class ShippingForm
{
    public int LocationId { get; set; }

    public string? LocationName { get; set; }

    public int AddresseeId { get; set; }

    public string? AddresseeName { get; set; }

    public string? AddresseeEmail { get; set; }

    public string? Observations { get; set; }

    public List<ShippingDetail> Details { get; set; } = new List<ShippingDetail>();

    public bool HasValidDestination =>
        LocationId > 0 &&
        !string.IsNullOrEmpty(LocationName) &&
        !string.IsNullOrEmpty(AddresseeEmail) &&
        AddresseeId > 0 &&
        new EmailAddressAttribute().IsValid(AddresseeEmail) &&
        !string.IsNullOrEmpty(AddresseeName);

    public bool HasValidDetails =>
        Details != null &&
        Details.Any() &&
        Details.All(d =>
            !string.IsNullOrEmpty(d.Type) &&
            !string.IsNullOrEmpty(d.Comment) &&
            d.Quantity > 0);
}