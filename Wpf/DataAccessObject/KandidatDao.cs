using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.DataAccessObject
{
    public class KandidatDao
    {
        HttpClient client = new HttpClient();

        string url = "Kandidat";
        public KandidatDao()
        {
            client.BaseAddress = new Uri("http://localhost:5000/api/");
        }

        //GET
        public List<Kandidat> GET()
        {
            HttpResponseMessage response = client.GetAsync(url).Result;

            if(response.IsSuccessStatusCode)
            {
                var KandidatString = response.Content.ReadAsStringAsync().Result;
                List<Kandidat> kandidati = JsonConvert.DeserializeObject<List<Kandidat>>(KandidatString);
                return kandidati;
            }
            return new List<Kandidat>();
        }

        //SAVE
        public async Task<bool> SAVE(Kandidat kandidat)
        {
            if(kandidat.KandidatId == 0)
            {
                //POST
                try
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress + url))
                    {
                        var json = JsonConvert.SerializeObject(kandidat);

                        using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                        {
                            request.Content = stringContent;

                            using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                            {
                                response.EnsureSuccessStatusCode();
                            }
                        }
                    }
                    return true;
                }
                catch (Exception xcp)
                {
                    Console.WriteLine(xcp.Message);
                    return false;
                }
            }
            else
            {
                //PUT
                try
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Put, client.BaseAddress + url))
                    {
                        var json = JsonConvert.SerializeObject(kandidat);

                        using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                        {
                            request.Content = stringContent;

                            using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                            {
                                response.EnsureSuccessStatusCode();
                            }
                        }
                    }
                    return true;
                }
                catch (Exception xcp)
                {
                    Console.WriteLine(xcp.Message);
                    return false;
                }
            }
        }

        //DELETE
        public void DELETE(Kandidat kandidat)
        {
            try
            {
                var response = client.DeleteAsync("Kandidat/" + kandidat.KandidatId).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success");
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }
            catch (Exception xcp)
            {

                Console.WriteLine(xcp.Message); ;
            }
        }
    }
}
