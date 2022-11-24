using System.Net.Http;
using System.Threading.Tasks;

namespace TrabauClassLibrary.Interfaces
{
    public interface IHttpsProxy
    {
        Task<string> CreateAsync(string url, HttpMethod httpMethod, string content);
    }
}
