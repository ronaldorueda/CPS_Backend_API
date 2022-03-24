namespace CPS_Backend_API.Models
{
    //Original JSON structure to be parsed by custom endpoint
    public class OldCalendarModel
    {
        public int CalendarID { get; set; }
        public string CalendarName { get; set; }
        public string ColorCode { get; set; }
        public int TagID { get; set; }
        public int ParentID { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
