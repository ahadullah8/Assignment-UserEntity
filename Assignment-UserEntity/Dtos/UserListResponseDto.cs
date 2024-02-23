namespace Assignment_UserEntity.Dtos
{
    public class UserListResponseDto
    {
        public List<UserDto> Users { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
