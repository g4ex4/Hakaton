using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hakaton.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Hakaton.Application.Users.Queries.GetUserList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery,
        UserListVm>
    {
        private readonly IHakatonDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetUserListQueryHandler ( IHakatonDbContext dbContext,
            IMapper mapper )
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<UserListVm> Handle (GetUserListQuery request,
            CancellationToken cancellationToken)
        {
            var usersQuery = await _dbContext.Users
                .Where(user => user.Id == request.Id)
                .ProjectTo<UserLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return new UserListVm { Users = usersQuery };
        }
    }
}
