using System;
using FluentValidation;
using Hakaton.Application.Users.Queries.GetUserList;

namespace Hakaton.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryValidator : AbstractValidator<GetNoteListQuery>
    {
        public GetNoteListQueryValidator()
        {
            //RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        }
    }
}
