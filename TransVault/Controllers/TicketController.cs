using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FutureWorkshopTicketSystem.Application.DTOs;
using FutureWorkshopTicketSystem.Application.Interfaces;
using FutureWorkshopTicketSystem.Common;

namespace FutureWorkshopTicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : BaseApiController
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var userIdClaim = User.FindFirstValue("userid");
            if (!int.TryParse(userIdClaim, out var userId))
                return ErrorResponse(System.Net.HttpStatusCode.BadRequest, "Invalid user ID");

            var result = await _ticketService.GetAll(userId);
            return OKResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(int id)
        {
            var ticket = await _ticketService.GetTicket(id);
            if (ticket == null) return ErrorResponse(System.Net.HttpStatusCode.BadRequest, $"Ticket with id={id} not found");
            return OKResponse(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketDTO dto)
        {
            dto.CreatedAt = DateTime.UtcNow;
           var ticket= await _ticketService.AddTicket(dto);
            return OKResponse(ticket);
        }

        [HttpPut]
        public async Task<IActionResult> Update(TicketDTO dto)
        {
            await _ticketService.UpdateTicket(dto);
            return OKResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _ticketService.DeleteTicket(id);
            if (!success) return ErrorResponse(System.Net.HttpStatusCode.BadRequest, $"Ticket with id={id} not found");
            return OKResponse();
        }
    }
}
