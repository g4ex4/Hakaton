﻿using MediatR;

namespace Hakaton.Application.Users.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<UserListVm>
    {
        public Guid Id { get; set; }
    }
}
