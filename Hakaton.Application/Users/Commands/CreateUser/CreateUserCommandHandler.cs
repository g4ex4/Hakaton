using Hakaton.Application.Interfaces;
using Hakaton.Domain;
using MediatR;

namespace Hakaton.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IHakatonDbContext _dbContext;
        public CreateUserCommandHandler (IHakatonDbContext dbContext)=>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken
            cancellationToken)
        {
            var user = new User
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                Role = request.Role
            };
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
  
            return user.Id;
        }
    }
}
