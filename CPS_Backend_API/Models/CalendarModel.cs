namespace CPS_Backend_API.Models
{
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
