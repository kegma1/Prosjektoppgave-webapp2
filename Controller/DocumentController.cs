using Microsoft.AspNetCore.Mvc;
using prosjekt_webapp2.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IFolderRepository _folderRepository;

        public DocumentController(IDocumentRepository documentRepository, IFolderRepository folderRepository, IConfiguration configuration)
        {
            _documentRepository = documentRepository;
            _folderRepository = folderRepository;
        }

        [HttpGet]
        public IActionResult GetDocuments()
        {
            var userId = GetCurrentUserId();
            if (userId == null) {
                return Unauthorized("Could not find ID in token");
            }

            var documents = _documentRepository.GetDocumentsByUser((int)userId);
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public IActionResult GetDocument(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == null) {
                return Unauthorized("Could not find ID in token");
            }

            var document = _documentRepository.GetDocumentById(id);

            if (document.UserId != userId) {
                return Unauthorized("You do not have permission to view this document");
            }

            return Ok(document);
        }

        [HttpPost]
        public IActionResult CreateDocument([FromBody] DocumentModel model)
        {
            var userId = GetCurrentUserId();
            if (userId == null) {
                return Unauthorized("Could not find ID in token");
            }

            var parentFolder = _folderRepository.GetSpecificFolder(model.FolderId);

            if (parentFolder.UserId != userId) {
                return Unauthorized("You do not have permission to create a file in this folder");
            }

            var document = new Document {
                Title = model.Name,
                Content = model.Content,
                ParentFolderId = model.FolderId,
                UserId = (int)userId,
                ContentTypeId = 1
            };

            var newDocument = _documentRepository.AddDocument(document);
            
            return CreatedAtAction(nameof(GetDocument), new { id = newDocument.Id }, newDocument);
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file, int folderId)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized("Could not find ID in token");
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            var parentFolder = _folderRepository.GetSpecificFolder(folderId);
            if (parentFolder.UserId != userId)
            {
                return Unauthorized("You do not have permission to upload a file to this folder.");
            }

            var allowedFileTypes = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedFileTypes.Contains(fileExtension))
            {
                return BadRequest("Invalid file type. Only JPG, JPEG, PNG, and GIF are allowed.");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine("uploads", fileName);
            Console.WriteLine(filePath);
            var absoluteFilePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            using (var stream = new FileStream(absoluteFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var document = new Document
            {
                Title = file.FileName,
                Content = filePath,
                ParentFolderId = folderId,
                UserId = (int)userId,
                ContentTypeId = 2,
                CreatedDate = DateTime.Now
            };

            var newDocument = _documentRepository.AddDocument(document);

            return CreatedAtAction(nameof(GetDocument), new { id = newDocument.Id }, newDocument);
        }

        [HttpGet("download/{id}")]
        public IActionResult DownloadImage(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized("Could not find ID in token");
            }

            var document = _documentRepository.GetDocumentById(id);

            if (document.UserId != userId)
            {
                return Unauthorized("You do not have permission to access this document.");
            }

            if (document.ContentTypeId != 2)
            {
                return BadRequest("The requested document is not an image.");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), document.Content);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileName = Path.GetFileName(filePath);

            return File(fileBytes, "application/octet-stream", fileName);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDocument(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized("Could not find ID in token");
            }

            var document = _documentRepository.GetDocumentById(id);

            if (document.UserId != userId)
            {
                return Unauthorized("You do not have permission to delete this document");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), document.Content);
            if (!string.IsNullOrEmpty(document.Content) && System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _documentRepository.DeleteDocument(document);

            return Ok(new { message = "Document deleted successfully" });
        }

        [NonAction]
        public int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            return string.IsNullOrEmpty(userIdClaim) ? null : int.Parse(userIdClaim);
        }
    }

    public class DocumentModel
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string FileType { get; set; }
        public int FolderId { get; set; }
    }
}


// {
//   "id": 1,
//   "username": "a",
//   "password": "a",
//   "email": "a@a.a"
// }