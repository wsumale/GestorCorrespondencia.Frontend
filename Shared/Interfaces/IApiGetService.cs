namespace GestorCorrespondencia.Frontend.Shared.Interfaces;
public interface IApiGetService
{
    Task<HttpResponseMessage> GetAsync(string url, int source = 1, bool log = false);
}
