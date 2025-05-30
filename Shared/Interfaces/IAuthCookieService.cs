using System.Threading.Tasks;
using GestorCorrespondencia.Frontend.Shared.Record;

namespace GestorCorrespondencia.Frontend.Shared.Interfaces;
public interface IAuthCookieService
{
    Task<AuthCookieResult?> GetAuthCookie(HttpResponseMessage response);
}