using Hakaton.Application.Common.Exceptions;
using Hakaton.Application.Interfaces;
using Hakaton.Domain;
using MediatR;

namespace Hakaton.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand>
    {
        private readonly IHakatonDbContext _dbContext;
        public DeleteUserCommandHandler(IHakatonDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteUserCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users
                .FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }
            _dbContext.Users.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
