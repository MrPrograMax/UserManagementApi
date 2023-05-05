using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Queries.GetUserDetails;
using Application.Users.Queries.GetUserList;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
        public async Task<ActionResult<UserListVm>> GetAll()
        {
            var query = new GetUserListQuery();
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}")]
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

        [HttpPut]
        public async Task<IActionResult> Delete([FromBody] DeleteUserDto deleteUserDto)
        {
            var command = _mapper.Map<DeleteUserCommand>(deleteUserDto);
            await Mediator.Send(command);

            return NoContent();
        }

    }
}
