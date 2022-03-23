using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using CPS_Backend_API.Models;

namespace CPS_Backend_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CPSController : Controller
    {
        private const string URL = "https://api.cps.edu/Calendar/cps/calendarslist";

        //Method to call CPS API
        public async Task<List<CalendarModel>> GetCalendars()
        {
            List<CalendarModel> calendars = new List<CalendarModel>();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("").Result;
            if(response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                calendars = JsonConvert.DeserializeObject<List<CalendarModel>>(responseBody);

                //Console.WriteLine("Calendar ID: {0}", temp.CalendarID);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            return calendars;
        }

        //API endpoint
        [HttpGet]
        public async Task<IEnumerable<CalendarModel>> GetAsync()
        {
            List<CalendarModel> calendars = await GetCalendars();

            return calendars.ToArray();
        }
    }
}
