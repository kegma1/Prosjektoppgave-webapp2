using Microsoft.EntityFrameworkCore;

namespace prosjekt_webapp2.Data.Repositories {
	public class DocumentRepository : IDocumentRepository {
		private readonly AppDbContext _context;

		public DocumentRepository(AppDbContext context) {
			_context = context;
		}

        public Document AddDocument(Document document)
        {
            _context.Document.Add(document);
			_context.SaveChanges();
			return document;
        }

        public void DeleteDocument(Document document)
        {
            _context.Document.Remove(document);
			_context.SaveChanges();
        }

        public IEnumerable<Document> GetAllDocuments() {
			return _context.Document
				.Include(b => b.ContentType)
				.Include(b => b.Owner)
				.Include(b => b.ParentFolder)
				.ToList();
		}

        public Document GetDocumentById(int id)
        {
            return _context.Document
				.Where(d => d.Id == id)
				.Include(b => b.ContentType)
				.Include(b => b.Owner)
				.Include(b => b.ParentFolder)
				.First();
        }

        public IEnumerable<Document> GetDocumentsByFolder(int id)
        {
            return _context.Document
				.Where(b => b.ParentFolderId == id)
				.Include(b => b.ContentType)
				.Include(b => b.Owner)
				.Include(b => b.ParentFolder)
				.ToList();
        }

        public IEnumerable<Document> GetDocumentsByUser(int id) {
			return _context.Document
				.Where(b => b.UserId == id)
				.Include(b => b.ContentType)
				.Include(b => b.Owner)
				.Include(b => b.ParentFolder)
				.ToList();
		}

        public Document UpdateDocument(Document document)
        {
            _context.Document.Update(document);
			_context.SaveChanges();
			return document;
        }
    }
}
