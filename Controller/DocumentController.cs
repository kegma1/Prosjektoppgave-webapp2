using Microsoft.AspNetCore.Mvc;
using prosjekt_webapp2.Data.Repositories;
using Microsoft.AspNetCore.Authorization;

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
                return Unauthorized("You do not have premission to view this document");
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
                return Unauthorized("You do not have premission to create a file in this folder");
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

        [HttpPut("{id}")]
        public IActionResult UpdateDocument(int id, [FromBody] DocumentModel model)
        {
            var userId = GetCurrentUserId();
            if (userId == null) {
                return Unauthorized("Could not find ID in token");
            }

            var document = _documentRepository.GetDocumentById(id);

            if (document.UserId != userId) {
                return Unauthorized("You do not have premission to update this document");
            }

            var parentFolder = _folderRepository.GetSpecificFolder(model.FolderId);

            if (parentFolder.UserId != userId) {
                return Unauthorized("You do not have premission to move this document to this folder");
            }

            document.Title = model.Name;
            document.Content = model.Content;
            document.ParentFolderId = model.FolderId;
            
            var newDocument = _documentRepository.UpdateDocument(document);
            
            return CreatedAtAction(nameof(GetDocument), new { id = newDocument.Id }, newDocument);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDocument(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == null) {
                return Unauthorized("Could not find ID in token");
            }

            var document = _documentRepository.GetDocumentById(id);

            if (document.UserId != userId) {
                return Unauthorized("You do not have premission to delete this document");
            }
            
            _documentRepository.DeleteDocument(document);

            return Ok(new { message = "Document deleted successfully" });
        }
        
        [NonAction]
        public int? GetCurrentUserId() {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim)) {
                return null;
            }

            return int.Parse(userIdClaim);
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