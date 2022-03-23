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
    [ApiController]
    [Route("[controller]")]
    public class CPSController : Controller
    {
        private const string URL = "https://api.cps.edu/Calendar/cps/calendarslist"; //takes 50-100ms to get response

        //Method to call CPS API, parse it and return a list of new model
        //currently taking 210ms-270ms - room for improvement to make it more efficient
        public async Task<List<CalendarModel>> GetCalendars()
        {
            List<CalendarModel> calendars = new List<CalendarModel>();
            List<OldCalendarModel> parsedCalendars = new List<OldCalendarModel>();

            //setting up to perform request to api
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //perform request and save it into a response
            HttpResponseMessage response = client.GetAsync("").Result;

            //check if response is valid - if so, parse it out into oldCalendarModel then loop through it to save into new model
            if(response.IsSuccessStatusCode)
            {
                //save response as a string to then use Newtonsoft's JSON nuget library to parse JSON into a oldCalendarModel list
                string responseBody = await response.Content.ReadAsStringAsync();
                parsedCalendars = JsonConvert.DeserializeObject<List<OldCalendarModel>>(responseBody);

                //loop through oldCalendarModel list, get its fields to put into temp CalendarModel and put into newModel list.
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
            }
            //print error code if CPS api is unreachable
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            return calendars;
        }

        //API endpoint - calls GetCalendars to return new list of calendars and returns it as an array so it is in JSON structure.
        [HttpGet]
        public async Task<IEnumerable<CalendarModel>> GetAsync()
        {
            List<CalendarModel> calendars = await GetCalendars();

            return calendars.ToArray();
        }
    }
}
