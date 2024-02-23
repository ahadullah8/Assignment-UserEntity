using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Models;
using Assignment_UserEntity.Repositories;
using Assignment_UserEntity.Repositories.Contrat;
using Assignment_UserEntity.Services.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Assignment_UserEntity.Services
{
    public class UserEntityService : IUserEntityService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        public UserEntityService(IMapper mapper, IUserRepository userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }
        public UserDto AddUser(UserDto newUser)
        {
            // map the input to user entity
            var toAdd = _mapper.Map<User>(newUser);

            // check if user already exists or not
            if (_userRepo.UserExists(toAdd))
            {
                throw new Exception("User already exists");
            }
            _userRepo.Add(toAdd);
            _userRepo.Save();
            return newUser;

        }

        public bool DeleteUser(string id)
        {
            //find user by id if found remove it from db
            var user = _userRepo.GetById(id);
            if (user is object)
            {
                _userRepo.Delete(user);
                _userRepo.Save();
                return true;
            }
            throw new Exception("User not found");

        }

        public async Task<UserListResponseDto> GetAllUserAsync(UserListParameters parameters)
        {
            // get a queryable
            var queryable = _userRepo.GetAll();

            // apply filtering based on the searchTerm
            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                queryable = queryable
                    .Where(u => EF.Functions.Like(u.UserName, $"%{parameters.SearchTerm}%")
                        || EF.Functions.Like(u.Email, $"%{parameters.SearchTerm}%")
                        || EF.Functions.Like(u.Address, $"%{parameters.SearchTerm}%")
                        || EF.Functions.Like(u.FullName, $"%{parameters.SearchTerm}%"));
            }

            // call sorting method
            if (!string.IsNullOrEmpty(parameters.SortBy))
            {
                // Sorting logic based on parameters.SortBy and parameters.IsSortAscending
                queryable = ApplySorting(queryable, parameters.SortBy, parameters.IsSortAscending);
            }

            // get total count
            var totalCount = await queryable.CountAsync();

            // apply paging
            var paginatedUsers = await queryable
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            // Create the response DTO
            var responseDto = new UserListResponseDto
            {
                Users = _mapper.Map<List<UserDto>>(paginatedUsers),
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / parameters.PageSize),
                CurrentPage = parameters.PageNumber,
                HasPreviousPage = parameters.PageNumber > 1,
                HasNextPage = parameters.PageNumber < (int)Math.Ceiling((double)totalCount / parameters.PageSize)
            };

            return responseDto;
        }

        public UserDto GetUser(string id)
        {
            //find user by id and check if user found or not
            var userDetails = _userRepo.GetById(id);
            if (userDetails is object)
            {
                return _mapper.Map<UserDto>(userDetails);
            }
            throw new Exception("User not found");
        }

        public UserDto UpdateUser(string id, UserDto updateUser)
        {
            //get the matching user and update its data
            var toUpdate = _userRepo.GetById(id);
            if (toUpdate is object)
            {
                toUpdate.Email = updateUser.Email;
                toUpdate.UserName = updateUser.UserName;
                toUpdate.FullName = updateUser.FullName;
                _userRepo.Update(toUpdate);
                _userRepo.Save();
                return updateUser;
            }
            throw new Exception("User not found");
        }

        //private methods
        private IQueryable<User> ApplySorting(IQueryable<User> queryable, string sortBy, bool isSortAscending)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                var property = typeof(User).GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null)
                {
                    var parameterExpression = Expression.Parameter(typeof(User), "u");
                    var propertyExpression = Expression.Property(parameterExpression, property);
                    var lambdaExpression = Expression.Lambda(propertyExpression, parameterExpression);

                    queryable = isSortAscending
                        ? Queryable.OrderBy(queryable, (dynamic)lambdaExpression)
                        : Queryable.OrderByDescending(queryable, (dynamic)lambdaExpression);
                }
            }

            return queryable;
        }

    }
}