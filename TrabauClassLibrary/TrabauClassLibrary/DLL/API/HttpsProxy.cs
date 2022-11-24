using System.Net.Http;
using TrabauClassLibrary.Interfaces;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using System.Net;

namespace TrabauClassLibrary.DLL.API
{
    public class HttpsProxy : IHttpsProxy
    {
        public async Task<string> CreateAsync(string url, HttpMethod httpMethod, string content)
        {
            try
            {
                var handler = new WinHttpHandler();
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var data = new StringContent(content, Encoding.UTF8, "application/json");

                    var request = new HttpRequestMessage
                    {
                        Method = httpMethod,
                        RequestUri = new Uri(url),
                        Content = data,
                    };

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();

                    string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    client.Dispose();
                    handler.Dispose();

                    return result;
                }
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
    }
}
