using System;
using System.Net;
using JishoCSharpWrapper.Shared.Models;
using WikiaCSharpWrapper.Models.API;

namespace WikiaCSharpWrapper
{
    public class Client
    {
        public static Message<Models.API.RootObject> RequestValuesFromWiki(string wikiaName, string value, bool autoLatinToKana)
        {
            Message<RootObject> message = new Message<RootObject>();

            try
            {
                if (!autoLatinToKana) value = $"\"{value}\"";

                var url = $"http://{wikiaName}.wikia.com/api/v1/Search/List?query={value}&minArticleQuality=0&batch=1&namespaces=0";

                WebClient webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
                webClient.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");

                webClient.Encoding = System.Text.Encoding.UTF8;

                string result = webClient.DownloadString(url);
                message = IO.Convert.JsonToObject(result);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return message;
        }
    }
}
