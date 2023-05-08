﻿using Application.Users.Queries.GetUserList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetAllUsers
{
    public class GetAllUserQuery : IRequest<AllUserListVm>
    {
    }
}
