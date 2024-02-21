namespace Assignment_UserEntity.Dtos
{
    public class ResponseDto
    {
        public int StatusCode { get; set; } 
        public string? Message { get; set; } = string.Empty;
        public bool? Success { get; set; }
        public object? Payload { get; set; } = null;
        public List<string>? Errors { get; set; } = null;
    }
}
