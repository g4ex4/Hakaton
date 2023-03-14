using AutoMapper;
using Hakaton.Application.Common.Exceptions;
using Hakaton.Application.Interfaces;
using Hakaton.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Hakaton.Application.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQueryHandler
        : IRequestHandler<GetUserDetailsQuery, UserDetailsVm>
    {
        private readonly IHakatonDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetUserDetailsQueryHandler(IHakatonDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDetailsVm> Handle(GetUserDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(user =>
            user.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id !=request.Id)
            {
                throw new NotFoundException (nameof(User), request.Id); 
            }
            return _mapper.Map<UserDetailsVm>(entity);
        }
    }
}
