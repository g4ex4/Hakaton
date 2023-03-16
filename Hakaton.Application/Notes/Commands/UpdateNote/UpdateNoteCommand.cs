using Hakaton.Domain;
using MediatR;

namespace Hakaton.Application.Users.Commands.UpdateUser
{
    public class UpdateNoteCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
