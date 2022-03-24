using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using CPS_Backend_API.Models;

namespace CPS_Backend_API.Controllers
{
    //API route will be hit by using the following http://localhost:[port of run]/CPS  --  CPS is the name of the Controller.
    [ApiController]
    [Route("[controller]")]
    public class CPSController : Controller
    {
        private const string URL = "https://api.cps.edu/Calendar/cps/calendarslist"; //takes 50-100ms to get response

        //Method to call CPS API, parse it and return a list of new model
        //currently taking 180ms-250ms - still room for improvement to make it more efficient
        public async Task<List<CalendarModel>> GetCalendars(HttpResponseMessage response)
        {
            List<CalendarModel> calendars = new List<CalendarModel>();
            List<OldCalendarModel> parsedCalendars = new List<OldCalendarModel>();

            //save response as a string to then use Newtonsoft's JSON nuget library to parse JSON into a `oldCalendarModel` list
            string responseBody = await response.Content.ReadAsStringAsync();
            parsedCalendars = JsonConvert.DeserializeObject<List<OldCalendarModel>>(responseBody);

            //Grab values from `oldCalendarModel` to assign to `CalendarModel` so it uses new `html_Hex_ColorCode`
            for (int i = 0; i < parsedCalendars.Count; i++)
            {
                CalendarModel tempParsedCalendar = new CalendarModel();

                tempParsedCalendar.CalendarID = parsedCalendars[i].CalendarID;
                tempParsedCalendar.CalendarName = parsedCalendars[i].CalendarName;
                tempParsedCalendar.html_Hex_ColorCode = parsedCalendars[i].ColorCode;
                tempParsedCalendar.TagID = parsedCalendars[i].TagID;
                tempParsedCalendar.ParentID = parsedCalendars[i].ParentID;
                tempParsedCalendar.Status = parsedCalendars[i].Status;
                tempParsedCalendar.Type = parsedCalendars[i].Type;

                calendars.Add(tempParsedCalendar);
            }

            return calendars;
        }

        //When CPSController is called through the API, it uses this method to perform HTTPGet
        //TODO: at the moment this only returns CalendarModel JSON, but need to implement so that custom status codes with messages  are sent to use if CPS API is unreachable
        [HttpGet]
        public async Task<IEnumerable<CalendarModel>> GetAsync()
        {
            List<CalendarModel> calendars = new List<CalendarModel>();

            //setting up to perform request to api
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //Perform, save, evaluate, and return relavent Calendar information
            HttpResponseMessage response = client.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                //If valid response, saved/return modified version in new model
                calendars = await GetCalendars(response);
                return calendars.ToArray();
            }
            else
            {
                //If CPS api is unreachable, print error code
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return calendars;
            }
        }

    }
}
