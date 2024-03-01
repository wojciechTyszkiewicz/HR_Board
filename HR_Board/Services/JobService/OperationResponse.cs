namespace HR_Board.Services.JobService
{
    public class OperationResponse
    {
        public bool Success { get; set; }
        public OperationResponseStatus ResponseStatus { get; set; }
        public OperationResponse(bool success, OperationResponseStatus responseStatus)
        {
            Success = success;
            ResponseStatus = responseStatus;
        }
    }


    public enum OperationResponseStatus 
    { 
        NotFound,
        Forbiden,
        Success
    }
}
