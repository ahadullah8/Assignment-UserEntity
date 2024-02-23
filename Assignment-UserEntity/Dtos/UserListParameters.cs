using Microsoft.AspNetCore.Mvc;

namespace Assignment_UserEntity.Dtos
{
    public class UserListParameters
    {
        [FromQuery(Name = "searchTerm")]
        public string SearchTerm { get; set; }

        [FromQuery(Name = "pageNumber")]
        public int PageNumber { get; set; } = 1;

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = 5;

        [FromQuery(Name = "sortBy")]
        public string SortBy { get; set; } = "username";

        [FromQuery(Name = "isSortAscending")]
        public bool IsSortAscending { get; set; } = true;
    }
}
