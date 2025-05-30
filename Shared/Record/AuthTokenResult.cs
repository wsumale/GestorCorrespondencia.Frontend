namespace GestorCorrespondencia.Frontend.Shared.Record;
public record AuthTokenResult(string? AccessToken, string? exp, string? RefreshToken, string? expRefreshToken);