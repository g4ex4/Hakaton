using MediatR;

namespace Hakaton.Application.Users.Queries.GetUserList
{
    public class GetNoteListQuery : IRequest<NoteListVm>
    {
        public Guid? UserId { get; set; }
    }   
}
