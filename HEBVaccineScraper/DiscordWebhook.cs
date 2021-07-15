using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://discord.com/developers/docs/resources/webhook

namespace HEBVaccineScraper
{
    public class DiscordWebhook
    {
        public const int MAX_CONTENT_CHAR_COUNT = 2000;

        public string content { get; set; }
        public string username { get; set; }
        public string avatar_url { get; set; }
        public bool tts { get; set; }
        public byte[] file { get; set; }
        public string payload_json { get; set; }
    }
}
