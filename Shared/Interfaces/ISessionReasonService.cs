namespace GestorCorrespondencia.Frontend.Shared.Interfaces;
public interface ISessionReasonService
{
    Task SetSessionReasonAsync(string reason);
}