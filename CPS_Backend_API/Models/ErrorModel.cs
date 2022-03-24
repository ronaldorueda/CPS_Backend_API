namespace CPS_Backend_API.Models
{
    //Utility model for informing end user of endpoint status
    public class ErrorModel
    {
        public string Message { get; set; }
        public string Code { get; set; }
    }
}
