using Application.Common.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHendler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserManagementDbContext _dbContext;

        public DeleteUserCommandHendler(IUserManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FindAsync(new object[] { request.UserId }, cancellationToken);

            if (user == null || user.Id != request.UserId)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            var blockedGroup = await _dbContext.UserGroups
                .FirstOrDefaultAsync(g => g.Code == "Blocked", cancellationToken);

            if (blockedGroup == null)
            {
                throw new NotFoundException(nameof(UserGroup), "Blocked");
            }

            user.UserStateId = blockedGroup.Id;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
