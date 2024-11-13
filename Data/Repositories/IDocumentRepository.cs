namespace prosjekt_webapp2.Data.Repositories {
	public interface IDocumentRepository {

		public IEnumerable<Document> GetAllDocuments();
		public IEnumerable<Document> GetDocumentsByUser(int id);
		public IEnumerable<Document> GetDocumentsByFolder(int id);
		public Document GetDocumentById(int id);
		public Document AddDocument(Document document);
		public void DeleteDocument(Document document);
		public Document UpdateDocument(Document document);
	}
}
