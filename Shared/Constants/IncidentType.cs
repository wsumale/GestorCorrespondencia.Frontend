namespace GestorCorrespondencia.Frontend.Shared.Constants;
public enum IncidentType
{
    NoReceived = 1,
    ReturnedToCorrespondence = 2,
    ReturnedToSender = 3,
    ReceiverChangedInCorrespondence = 4,
    ReceiverChangedInDestination = 5,
}

public static class IncidentTypeExtensions
{
    public static string Description(this IncidentType incident)
    {
        return incident switch
        {
            IncidentType.NoReceived => "No Recibido",
            IncidentType.ReturnedToCorrespondence => "Devuelto a Correspondencia",
            IncidentType.ReturnedToSender => "Devuelto al Remitente",
            IncidentType.ReceiverChangedInCorrespondence => "Cambio de Destinatario en Correspondencia",
            IncidentType.ReceiverChangedInDestination => "Cambio de Destinatario en Destino",
            _ => throw new ArgumentOutOfRangeException(nameof(incident), incident, null)
        };
    }
}