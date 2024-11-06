using Microsoft.EntityFrameworkCore;

namespace prosjekt_webapp2.Data.Repositories {
	public class DocumentRepository : IDocumentRepository {
		private readonly AppDbContext _context;

		public DocumentRepository(AppDbContext context) {
			_context = context;
		}

		public IEnumerable<Document> GetAllDocuments() {
			return _context.Document.Include(b => b.ContentType).Include(b => b.Owner).ToList();
		}

		public IEnumerable<Document> GetDocumentsByUser(int id) {
			return _context.Document.Where(b => b.UserId == id).Include(b => b.ContentType).Include(b => b.Owner).ToList();
		}
	}
}
