using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetFolders()
        {
            var folders = new List<string> { "Folder1", "Folder2" };
            return Ok(folders);
        }

        [HttpGet("{id}")]
        public IActionResult GetFolder(int id)
        {
            return Ok(new { id, name = "Sample Folder" });
        }

        [HttpPost]
        public IActionResult CreateFolder([FromBody] FolderModel model)
        {
            return Created("", new { message = "Folder created successfully" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFolder(int id, [FromBody] FolderModel model)
        {
            return Ok(new { message = "Folder updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFolder(int id)
        {
            return Ok(new { message = "Folder deleted successfully" });
        }
    }

    public class FolderModel
    {
        public string Name { get; set; }
    }
}
