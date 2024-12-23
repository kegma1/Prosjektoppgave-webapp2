using Microsoft.AspNetCore.Mvc;
using prosjekt_webapp2.Data.Repositories;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Controller
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
	public class FolderController : ControllerBase
    {
		private readonly IFolderRepository _folderRepository;
		private readonly IDocumentRepository _documentRepository;

		public FolderController(IFolderRepository folderRepository, IDocumentRepository documentRepository, IConfiguration configuration) {
			_folderRepository = folderRepository;
			_documentRepository = documentRepository;
		}

		[HttpGet]
        public IActionResult GetFolders()
        {
			var userId = GetCurrentUserId();
			if (userId == null) {
				return Unauthorized("Could not find ID in token");
			}

			var folders = _folderRepository.GetUserFolders((int)userId);
            return Ok(folders);
        }

        [HttpGet("{id}")]
        public IActionResult GetFolder(int id)
        {
            var userId = GetCurrentUserId();
			if (userId == null) {
				return Unauthorized("Could not find ID in token");
			}

            var folder = _folderRepository.GetSpecificFolder(id);

			if (folder.UserId != userId) {
				return Unauthorized("You do not have premission to view this folder");
			}

			return Ok(folder);
        }

		[HttpGet("content/{id}")]
        public IActionResult GetFoldercontent(int id)
        {
            var userId = GetCurrentUserId();
			if (userId == null) {
				return Unauthorized("Could not find ID in token");
			}

            var folder = _folderRepository.GetSpecificFolder(id);

			if (folder.UserId != userId) {
				return Unauthorized("You do not have premission to view this folder");
			}

			var subFolders = _folderRepository.GetSubFolders(folder);
			var files = _documentRepository.GetDocumentsByFolder(folder.Id);

			return Ok(new { folders = subFolders, documents = files});
        }

        [HttpPost]
        public IActionResult CreateFolder([FromBody] FolderModel model)
        {
			var userId = GetCurrentUserId();
			if (userId == null) {
				return Unauthorized("Could not find ID in token");
			}

			var parentFolder = _folderRepository.GetSpecificFolder(model.parentId);

			if (parentFolder.UserId != userId) {
				return Unauthorized("You do not have premission to create a folder in this folder");
			}

			var folder = new Folder{
				Name = model.Name,
				ParentFolderId = model.parentId,
				UserId = (int)userId,
			};

			var newFolder = _folderRepository.AddFolder(folder);

			return CreatedAtAction(nameof(GetFolder), new { id = newFolder.Id }, newFolder);
		}

        [HttpPut("{id}")]
        public IActionResult UpdateFolder(int id, [FromBody] FolderModel model)
        {
			var userId = GetCurrentUserId();
			if (userId == null) {
				return Unauthorized("Could not find ID in token");
			}

			var folder = _folderRepository.GetSpecificFolder(id);
			if (folder.UserId != userId) {
				return Unauthorized("You do not have premission to update this folder");
			}

			folder.Name = model.Name;
			folder.ParentFolderId = model.parentId;

			var newFolder = _folderRepository.UpdateFolder(folder);

			return CreatedAtAction(nameof(GetFolder), new { id = newFolder.Id }, newFolder);
		}

        [HttpDelete("{id}")]
        public IActionResult DeleteFolder(int id)
        {
			var userId = GetCurrentUserId();
			if (userId == null) {
				return Unauthorized("Could not find ID in token");
			}

			var folder = _folderRepository.GetSpecificFolder(id);
			if (folder.UserId != userId) {
				return Unauthorized("You do not have premission to delete this folder");
			}

			_folderRepository.DeleteFolder(folder);

			return Ok(new { message = "Folder deleted successfully" });
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

	public class FolderModel
    {
        public string Name { get; set; }
		public int parentId { get; set; }
    }
}
