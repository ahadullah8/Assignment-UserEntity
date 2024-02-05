namespace Assignment_UserEntity.Dtos
{
    public class GenericResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; } = null;
        public List<string>? Error { get; set; } = null;
    }
}
