using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransVault.Application.DTOs;
using TransVault.Application.Interfaces;
using TransVault.Common;

namespace TransVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AttachmentController : BaseApiController
    {
        private readonly IAttachmentService _attachmentService;
        private readonly string _uploadRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "attachments");

        public AttachmentController(IAttachmentService service)
        {
            _attachmentService = service;
        }

        [HttpPost("{ticketId}")]
        public async Task<IActionResult> UploadAttachment(int ticketId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return ErrorResponse(System.Net.HttpStatusCode.BadRequest, "File is required");

            Directory.CreateDirectory(_uploadRoot);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(_uploadRoot, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var dto = new AttachmentDTO
            {
                TicketId = ticketId,
                FileName = file.FileName,
                FilePath = $"/attachments/{fileName}"
            };

            var attachment=await _attachmentService.AddAttachment(dto);
            return OKResponse(attachment);
        }

        [HttpGet("ticket/{ticketId}")]
        public async Task<IActionResult> GetByTicketId(int ticketId)
        {
            var result = await _attachmentService.GetAttachmentsByTicketId(ticketId);
            return OKResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _attachmentService.DeleteAttachment(id);
            if (!success) return ErrorResponse(System.Net.HttpStatusCode.BadRequest, $"Attachment with id={id} not found");
            return OKResponse();
        }
    }
}
