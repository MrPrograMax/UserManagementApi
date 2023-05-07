using Application.Authentication.BaseAuth;
using Application.Common.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Authentication.CodeAuth
{
    public class AuthUserByCodeQueryHendler : IRequestHandler<AuthUserByCodeQuery, bool>
    {
        private readonly IUserManagementDbContext _dbContext;

        public AuthUserByCodeQueryHendler(IUserManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(AuthUserByCodeQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == request.Login);

            if (user == null || user.Login != request.Login)
            {
                throw new NotFoundException(nameof(User), request.Login);
            }

            #region Шифрование пароля

            byte[] salt = user.Salt;

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

            if (encryptedPassword == user.Password)
            {
                var group = await _dbContext.UserGroups.FirstOrDefaultAsync(group => group.Code == request.GroupCode);
                return user.UserGroupId == group.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
