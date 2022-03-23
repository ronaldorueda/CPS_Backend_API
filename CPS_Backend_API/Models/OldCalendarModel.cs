namespace CPS_Backend_API.Models
{
    //old model used to deserialize CPS' API
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
