using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Library.eCommerce.Utilities
{
    public class WebRequestHandler
    {
        private string host = "localhost"; //http://10.185.208.107:5206/ToDo/1
        private string port = "5206"; //"7009"
        private HttpClient Client { get; }
        public WebRequestHandler()
        {
            Client = new HttpClient();
        }
        public async Task<string> Get(string url)
        {
            var fullUrl = $"http://{host}:{port}{url}";
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client
                        .GetStringAsync(fullUrl)
                        .ConfigureAwait(false);
                    return response;
                }
            } catch(Exception e)
            {

            }


            return null;
        }



                public async Task<string> Delete(string url)
                {
                    var fullUrl = $"http://{host}:{port}{url}";
                    try
                    {
                        using (var client = new HttpClient())
                        {
                            using (var request = new HttpRequestMessage(HttpMethod.Delete, fullUrl))
                            {
                                using (var response = await client
                                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                                        .ConfigureAwait(false))
                                {
                                    if (response.IsSuccessStatusCode)
                                    {
                                        return await response.Content.ReadAsStringAsync();
                                    }
                                    return "ERROR";
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                        Console.WriteLine($"Inner Exception: {e.InnerException?.Message}");
                        Console.WriteLine($"Stack Trace: {e.StackTrace}");
                        return null;
                    }



                    return null;
                }
                
   
   /*     
        public async Task<string> Delete(string url)
        {
            var fullUrl = $"http://{host}:{port}{url}";
            Console.WriteLine($"DELETE: Making request to {fullUrl}");

            try
            {
                using (var client = new HttpClient())
                {
                    // Add timeout to prevent hanging
                    client.Timeout = TimeSpan.FromSeconds(30);

                    using (var request = new HttpRequestMessage(HttpMethod.Delete, fullUrl))
                    {
                        Console.WriteLine($"DELETE: Sending request...");

                        using (var response = await client
                                .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                                .ConfigureAwait(false))
                        {
                            Console.WriteLine($"DELETE: Response status: {response.StatusCode}");
                            Console.WriteLine($"DELETE: Response reason: {response.ReasonPhrase}");

                            var content = await response.Content.ReadAsStringAsync();
                            Console.WriteLine($"DELETE: Response content: '{content}'");
                            Console.WriteLine($"DELETE: Content length: {content?.Length ?? 0}");

                            if (response.IsSuccessStatusCode)
                            {
                                Console.WriteLine("DELETE: Request succeeded, returning content");
                                return content;
                            }
                            else
                            {
                                Console.WriteLine($"DELETE: Request failed with status {response.StatusCode}");
                                return "ERROR";
                            }
                        }
                    }
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"DELETE: HTTP Error: {httpEx.Message}");
                Console.WriteLine($"DELETE: Is server running on {fullUrl}?");
                return null;
            }
            catch (TaskCanceledException tcEx)
            {
                Console.WriteLine($"DELETE: Request timeout: {tcEx.Message}");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"DELETE: Unexpected error: {e.Message}");
                Console.WriteLine($"DELETE: Stack trace: {e.StackTrace}");
                return null;
            }
        }
*/
        public async Task<string> Post(string url, object obj)
        {
            var fullUrl = $"http://{host}:{port}{url}";
            try
            {
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Post, fullUrl))
                    {
                        var json = JsonConvert.SerializeObject(obj);
                        using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                        {
                            request.Content = stringContent;

                            using (var response = await client
                                .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                                .ConfigureAwait(false))
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    return await response.Content.ReadAsStringAsync();
                                }
                                return "ERROR";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Exception: {e.InnerException?.Message}");
                Console.WriteLine($"Stack Trace: {e.StackTrace}");
                return null;
            }

        }
    }
}