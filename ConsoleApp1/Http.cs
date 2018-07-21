using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class Http
    {
        public static string Get(string url)
        {
            HttpClientHandler myHandler = new HttpClientHandler();

            CustomHandler1 myHandler1 = new CustomHandler1();

            //myHandler1.InnerHandler = myHandler2;

            HttpContent content = new MultipartContent();
            content.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("Authorization", "aaa"));

            HttpContent content1 = new StringContent("一段json", Encoding.UTF8, "application/json");

            HttpContent httpContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes("qqqqq"));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("text/xml");//代表发送端（客户端|服务器）发送的实体数据的数据类型。 

            // 构造POST参数
            HttpContent content2 = new FormUrlEncodedContent(new Dictionary<string, string>()
           {
              {"email", "1"},
              {"pwd","11"}
           });



            var httpClient = new HttpClient(myHandler);
            //直接设置头
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic ");
            httpClient.DefaultRequestHeaders.Accept.Clear(); //Accept代表发送端（客户端）希望接受的数据类型。 
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponse = httpClient.GetAsync(url).Result;

            httpClient.PostAsync(url, content1);



            return null;
        }

        public static string PostRequest(string url, HttpContent data)
        {
            var handler = new HttpClientHandler() { UseCookies = false };
            HttpClient client = new HttpClient(handler);
            var message = new HttpRequestMessage(HttpMethod.Post, url);
            message.Headers.Add("Cookie", "HttpContext.Current.Request.Headers[\"cookie\"]");
            message.Content = data;
            var response = client.SendAsync(message).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync().Result;
            return result;
        }

        //RemoteRequest("", new FormUrlEncodedContent(new Dictionary<string, string>(){}))
        public static string RemoteRequest(string url, HttpContent data)
        {
            var handler = new HttpClientHandler() { UseCookies = false };
            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetRemoteToken());
            var message = new HttpRequestMessage(HttpMethod.Post, url);
            message.Content = data;
            var response = client.SendAsync(message).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync().Result;
            return result;
        }

        public static string GetRemoteToken()
        {
            var parameters = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", "8f021caa13e64597a38e37f9a8521b89" },
                { "client_secret", "uds/s4u/GAWkCGkiRjTnake5UPsxQfi6hIzOuMTgMpM=" },
                { "redirect_uri", "HttpContext.Current.Request.Url.ToString()" }
            };
            HttpClient client = new HttpClient();
            var message = new HttpRequestMessage(HttpMethod.Post, "remoteApiUrl" + "/token");
            message.Content = new FormUrlEncodedContent(parameters);
            var response = client.SendAsync(message).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync().Result;
            var token = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            return token["access_token"];
        }

    }


    public class CustomHandler1 : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization == null)
            {
              
                //return Task.FromResult("");
            }

            Debug.WriteLine("Processing request in Custom Handler 1");
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            Debug.WriteLine("Processing response in Custom Handler 1");
            return response;
        }
    }
}
