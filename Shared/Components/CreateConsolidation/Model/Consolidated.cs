namespace GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.Model;
public class Consolidated
{
    public int Type { get; set; }
    public int RecipientLocationId { get; set; }
    public string? RecipientLocation { get; set; }
    public IList<ConsolidatedDetail> ConsolidatedDetail { get; set; }
}