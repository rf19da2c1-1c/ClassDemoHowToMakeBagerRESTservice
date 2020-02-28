using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BagerLib.model;
using Newtonsoft.Json;

namespace KageApp
{
    internal class KagerWorker
    {
        private string URI = "http://localhost:50915/api/Kager/";

        public async void Start()
        {
            IList<Kage> kager = await HentAlleKager();

            foreach (Kage kage in kager)
            {
                Console.WriteLine(kage);
            }

            Kage k = new Kage("rumkugle", 45, 5);
            bool erOprettet = await OpretKager(k);
            Console.WriteLine("Rumkugle er oprettet " + erOprettet);
        }

        private async Task<IList<Kage>> HentAlleKager()
        {
            List<Kage> kager;

            using (HttpClient client = new HttpClient())
            {
                //HttpResponseMessage res = await client.GetAsync(URI);
                String json = await client.GetStringAsync(URI);
                kager = JsonConvert.DeserializeObject<List<Kage>>(json);
            }

            return kager;
        }

        private async Task<bool> OpretKager(Kage kage)
        {
            bool ok = false;

            using (HttpClient client = new HttpClient())
            {
                // lave Body
                String jString = JsonConvert.SerializeObject(kage);
                StringContent content = new StringContent(jString, Encoding.UTF8, "application/json");
                HttpResponseMessage result  = await client.PostAsync(URI, content);

                if (result.IsSuccessStatusCode)
                {
                    string okRes = await result.Content.ReadAsStringAsync();
                    ok = JsonConvert.DeserializeObject<bool>(okRes);
                }
            }

            return ok;

        }

    }
}