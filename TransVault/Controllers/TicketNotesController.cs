using Microsoft.AspNetCore.Mvc;
using TransVault.Application.DTOs;
using TransVault.Application.Interfaces;
using TransVault.Common;

namespace TransVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TicketNotesController : BaseApiController
    {
        private readonly ITicketNoteService _ticketNoteService;

        public TicketNotesController(ITicketNoteService ticketNoteService)
        {
            _ticketNoteService = ticketNoteService;
        }

        // GET: api/TicketNotes/ByTicket/5
        [HttpGet("ByTicket/{ticketId}")]
        public async Task<IActionResult> GetNotesByTicket(int ticketId)
        {
            var notes = await _ticketNoteService.GetNotesByTicketId(ticketId);
            return OKResponse(notes);
        }

        // GET: api/TicketNotes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote(int id)
        {
            var note = await _ticketNoteService.GetNote(id);
            if (note == null)
                return ErrorResponse(System.Net.HttpStatusCode.BadRequest, $"TicketNote with id={id} not found");

            return OKResponse(note);
        }

        // POST: api/TicketNotes
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketNoteDTO dto)
        {
            var ticketNote=await _ticketNoteService.AddNote(dto);
            return OKResponse(ticketNote);
        }

        // PUT: api/TicketNotes/5
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TicketNoteDTO dto)
        {
            await _ticketNoteService.UpdateNote(dto);
            return OKResponse();
        }

        // DELETE: api/TicketNotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _ticketNoteService.DeleteNote(id);
            if (!success)
                return ErrorResponse(System.Net.HttpStatusCode.BadRequest, $"TicketNote with id={id} not found");

            return OKResponse();
        }
    }
}
