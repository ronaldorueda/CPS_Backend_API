﻿namespace CPS_Backend_API.Models
{
    //new JSON structure that will be returned at my endpoint.
    public class CalendarModel
    {
        public int CalendarID { get; set; }
        public string CalendarName { get; set; }
        public string html_Hex_ColorCode { get; set; }
        public int TagID { get; set; }
        public int ParentID { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
