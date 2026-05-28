using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UploadImage(
            IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            var allowedExtensions =
                new[] { ".jpg", ".jpeg", ".png" };

            var extension =
                Path.GetExtension(file.FileName)
                    .ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest(
                    "Invalid image format");
            }

            var fileName =
                $"{Guid.NewGuid()}{extension}";

            var uploadsFolder =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "Uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(
                    uploadsFolder);
            }

            var filePath =
                Path.Combine(
                    uploadsFolder,
                    fileName);

            using var stream =
                new FileStream(
                    filePath,
                    FileMode.Create);

            await file.CopyToAsync(stream);

            var imageUrl =
                $"{Request.Scheme}://" +
                $"{Request.Host}/Uploads/{fileName}";

            return Ok(new
            {
                ImageUrl = imageUrl
            });
        }
    }
}