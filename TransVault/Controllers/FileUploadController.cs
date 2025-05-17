using Microsoft.AspNetCore.Mvc;

namespace TransVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        [HttpPost("{ticketId}")]
        public async Task<IActionResult> UploadFile(int ticketId, IFormFile file)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "attachments");
            Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // TODO: Save path to DB against ticketId in Attachment entity

            return Ok(new { filePath });
        }
    }
}
