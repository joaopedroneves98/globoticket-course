namespace GloboTicket.TicketManagement.Application.Responses
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            this.Success = true;
        }
        public BaseResponse(string message)
        {
            this.Success = true;
            this.Message = message;
        }

        public BaseResponse(string message, bool success)
        {
            this.Success = success;
            this.Message = message;
        }

        public bool Success { get; set; }
        
        public string Message { get; set; } = string.Empty;
        
        public List<string>? ValidationErrors { get; set; }
    }
}
