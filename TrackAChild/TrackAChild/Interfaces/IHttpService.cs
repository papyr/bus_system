using System.Threading.Tasks;

namespace TrackAChild.Interfaces
{
    internal interface IHttpService
    {
        Task<string> SendGetRequest(string param);
    }
}
