namespace GestorCorrespondencia.Frontend.Shared.Components.PreviewNewConsolidation.Model;
public class Consolidated
{
    public int Type { get; set; }
    public int RecipientLocationId { get; set; }
    public required IList<ConsolidatedDetail> ConsolidatedDetail { get; set; }
}