﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Application.Users.Queries.GetUserDetails
{
    public class GetNoteDetailsQuery: IRequest<NoteDetailsVm>
    {
        public Guid? UserId { get; set; }
        public Guid Id { get; set; }
    }
}
