using FluentValidation;
using Hakaton.Application.Users.Commands.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(createNoteCommand =>
                createNoteCommand.Title).NotEmpty().MaximumLength(250);
            RuleFor(createNoteCommand =>
                createNoteCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}
