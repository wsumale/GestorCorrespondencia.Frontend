namespace GestorCorrespondencia.Frontend.Shared.Constants;
public enum State
{
    NewShipment = 4,
    SentToCorrespondence = 5,
    ReceivedByCorrespondence = 6,
    NotReceived = 7,
    SentToDestination = 8,
    ReceiverChangePending = 9,
    AtReception = 10,
    Received = 11,
    ReturnedToCorrespondence = 12,
    ReturnedToSender = 13
}

public static class StateExtensions
{
    public static string Description(this State state)
    {
        return state switch
        {
            State.NewShipment => "Nuevo envío",
            State.SentToCorrespondence => "Enviado a correspondencia",
            State.ReceivedByCorrespondence => "Recibido por correspondencia",
            State.NotReceived => "No recibido",
            State.SentToDestination => "Enviado a destino",
            State.ReceiverChangePending => "Cambio pendiente destinatario",
            State.AtReception => "En recepción",
            State.Received => "Recibido",
            State.ReturnedToCorrespondence => "Devuelto a correspondencia",
            State.ReturnedToSender => "Devuelto al remitente",
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }
}