using AutoMapper;
using Hakaton.Application.Users.Commands.CreateUser;
using Hakaton.Application.Users.Commands.DeleteUser;
using Hakaton.Application.Users.Commands.UpdateUser;
using Hakaton.Application.Users.Queries.GetUserDetails;
using Hakaton.Application.Users.Queries.GetUserList;
using Hakaton.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hakaton.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class NoteController : BaseController
    {
        private readonly IMapper _mapper;

        public NoteController(IMapper mapper) => _mapper = mapper;
        [HttpGet("GetAll")]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<NoteDetailsVm>> Get([FromBody] Guid id)
        {
            var query = new GetNoteDetailsQuery
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
        {
            var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            var command = new DeleteNoteCommand
            {
                Id = id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
