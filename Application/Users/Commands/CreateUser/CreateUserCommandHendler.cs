using Application.Common.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHendler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserManagementDbContext _dbContext;

        public CreateUserCommandHendler(IUserManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(5000, cancellationToken); // задержка на 5 секунд

            #region Валидация

            var userExists = await _dbContext.Users.AnyAsync(u => u.Login == request.Login, cancellationToken);
                
            if (userExists)
            {
                throw new UserAlreadyExists(request.Login);
            }

            var adminExists = await _dbContext.Users.AnyAsync(u => u.UserGroup.Code == "Admin", cancellationToken);

            var adminGroup = await _dbContext.UserGroups
                .FirstOrDefaultAsync(g => g.Code == "Admin", cancellationToken);

            if (adminExists && request.UserGroupId == adminGroup.Id)
            {
                throw new AdminAlreadyExists();
            }

            #endregion

            #region Шифрование пароля

            byte[] salt = GenerateSalt();

            byte[] inputBytes = Encoding.UTF8.GetBytes(request.Password);
            byte[] saltedBytes = new byte[inputBytes.Length + salt.Length];
            Buffer.BlockCopy(inputBytes, 0, saltedBytes, 0, inputBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedBytes, inputBytes.Length, salt.Length);

            string encryptedPassword = String.Empty;

            using (var md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(saltedBytes);
                encryptedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }

            #endregion

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Login = request.Login,
                Password = encryptedPassword,
                CreatedDate = DateTime.UtcNow,
                UserGroupId = request.UserGroupId,
                Salt = salt
            };

            var activeState = await _dbContext.UserStates
                .FirstOrDefaultAsync(g => g.Code == "Active", cancellationToken);

            user.UserStateId = activeState.Id;

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }

        private static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16]; 
                rng.GetBytes(salt); 
                return salt;
            }
        }
    }
}
