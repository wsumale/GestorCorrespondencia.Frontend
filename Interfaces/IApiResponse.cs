namespace Unisuper.GestorCorrespondencia.Frontend.Interfaces
{
    public interface IApiResponse<T>
    {
        bool Success { get; }
        string Message { get; }
        int Code { get; }
        T? Data { get; }
    }

}
