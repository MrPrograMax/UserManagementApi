using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Queries.GetAllUsers;
using Application.Users.Queries.GetUserDetails;
using Application.Users.Queries.GetUserList;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Authentication;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [BasicAuthentication]
        public async Task<ActionResult<UserListVm>> GetAll()
        {
            var query = new GetAllUserQuery();
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        [BasicAuthentication]
        public async Task<ActionResult<UserListVm>> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetUserListQuery { CurrentPage = pageNumber, PageSize = pageSize };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        [BasicAuthentication]
        public async Task<ActionResult<UserListVm>> Get(Guid id)
        {
            var query = new GetUserDetailsQuery
            {
                UserId = id
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateUserDto createUserDto)
        {
            var command = _mapper.Map<CreateUserCommand>(createUserDto);
            var userId = await Mediator.Send(command);

            return Ok(userId);
        }

        [HttpDelete("{id}")]
        [BasicAuthentication("Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUserCommand { UserId = id };
                
            await Mediator.Send(command);

            return NoContent();
        }

    }
}
