using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Timers;

namespace HEBVaccineScraper
{
    class Program
    {
        static string discordWebhookURL = "";
        static List<string> selectedCities = new List<string>() { "Austin"};
        const int MIN_SLOTS_BEFORE_ALERT = 3;

        static void Main(string[] args)
        {
            SetSelectedCitiesUpper();

            Timer timer = new Timer(RandomTime());
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;

            SearchAppointments();

            timer.Start();

            Console.ReadLine();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var found = SearchAppointments();

            ((Timer)sender).Interval = RandomTime(found ? 95 : 0);
        }

        public static int RandomTime(int extraSecs = 0)
        {
            var minSecs = 11;
            var maxSecs = 39;

            var rand = new Random(DateTime.Now.Millisecond);
            return rand.Next(minSecs * 1000, maxSecs * 1000) + (extraSecs * 1000);
        }

        public static bool SearchAppointments()
        {
            WriteCurrentSearchTime();

            var client = new WebClient();
            var bytes = client.DownloadData(HEB.URL);
            var json = Encoding.Default.GetString(bytes);

            StringBuilder builder = new();

            var resp = JsonConvert.DeserializeObject<HEB.Root>(json);

            foreach (HEB.Location loc in resp.locations)
            {
                if (selectedCities.Contains(loc?.city.ToUpper()))
                {
                    if (loc.openAppointmentSlots >= MIN_SLOTS_BEFORE_ALERT)
                    {
                        builder.AppendLine(loc.name);
                        builder.AppendLine(loc.GetAddress());
                        builder.AppendLine($"Spots: {loc.openAppointmentSlots} | Time Slots: {loc.openTimeslots}\n");
                    }
                }
            }

            if(builder.Length > 0)
            {
                Service.SendSimpleDiscordMessage(builder.ToString().Trim(), discordWebhookURL);
                Console.WriteLine(builder.ToString());
            }
            else
            {
                Console.WriteLine("No appointments found for selected cities");
            }

            return builder.Length > 0;
        }

        public static void WriteCurrentSearchTime()
        {
            Console.WriteLine($"\nSearching at {DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt")}");
        }

        public static void SetSelectedCitiesUpper()
        {
            for(int i = 0; i < selectedCities.Count; i++)
            {
                selectedCities[i] = selectedCities[i].ToUpper();
            }
        }
    }
}
