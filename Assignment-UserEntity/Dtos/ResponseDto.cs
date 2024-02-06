namespace Assignment_UserEntity.Dtos
{
    public class ResponseDto
    {
        public int StatusCode { get; set; } 
        public string Message { get; set; } = string.Empty;
        public object? Payload { get; set; } = null;
    }
}
