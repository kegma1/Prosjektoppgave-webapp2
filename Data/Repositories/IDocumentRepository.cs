namespace prosjekt_webapp2.Data.Repositories {
	public interface IDocumentRepository {

		public IEnumerable<Document> GetAllDocuments();
		public IEnumerable<Document> GetDocumentsByUser(int id);
	}
}
