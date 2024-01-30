namespace Assignment_UserEntity.ResponseDTO
{
    public class GenericResponse<T>
    {
        public bool Status { get; set; }
        public T Body { get; set; }
        public string ErrorMessage { get; set; }
    }
}
