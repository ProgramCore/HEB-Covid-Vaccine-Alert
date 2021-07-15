using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HEBVaccineScraper
{
    public static class Service
    {
        public static void SendSimpleDiscordMessage(string message, string webhookURL)
        {
            if(string.IsNullOrWhiteSpace(message))
            { return; }

            for(int i = 1; i <= message.Length/DiscordWebhook.MAX_CONTENT_CHAR_COUNT + ((message.Length%DiscordWebhook.MAX_CONTENT_CHAR_COUNT > 0) ? 1 : 0); i++)
            {
                var start = (i - 1) * DiscordWebhook.MAX_CONTENT_CHAR_COUNT;
                var section = message.Substring(start, message.Length - start < DiscordWebhook.MAX_CONTENT_CHAR_COUNT ? message.Length - start : DiscordWebhook.MAX_CONTENT_CHAR_COUNT);

                var hook = new DiscordWebhook() { content = section };

                WebClient client = new WebClient();
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json; utf-8");
                client.Headers.Add(HttpRequestHeader.UserAgent, "DiscordWebhookForSmallText-ByBAY");
                client.UploadString(webhookURL, JsonConvert.SerializeObject(hook));
                client.Dispose();
            }
        }
    }
}
