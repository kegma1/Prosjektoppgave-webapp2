using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDocuments()
        {
            var documents = new List<string> { "Document1", "Document2" };
            return Ok(documents);
        }
        [HttpGet("{id}")]
        public IActionResult GetDocument(int id)
        {
            return Ok(new { id, name = "Sample Document" });
        }

        [HttpPost]
        public IActionResult CreateDocument([FromBody] DocumentModel model)
        {
            return Created("", new { message = "Document created successfully" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDocument(int id, [FromBody] DocumentModel model)
        {
            return Ok(new { message = "Document updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDocument(int id)
        {
            return Ok(new { message = "Document deleted successfully" });
        }
    }

    public class DocumentModel
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
