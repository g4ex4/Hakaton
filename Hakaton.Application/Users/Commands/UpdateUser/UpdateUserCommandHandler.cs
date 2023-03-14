using Hakaton.Application.Common.Exceptions;
using Hakaton.Application.Interfaces;
using Hakaton.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IHakatonDbContext _dbContext;
        public UpdateUserCommandHandler(IHakatonDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(user =>
            user.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException (nameof(User), request.Id);
            }
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Email = request.Email;
            entity.Password = request.Password;
            entity.Role = request.Role;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
