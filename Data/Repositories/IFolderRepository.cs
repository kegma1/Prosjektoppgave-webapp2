

namespace prosjekt_webapp2.Data.Repositories {
	public interface IFolderRepository {
		public IEnumerable<Folder> GetAllFolders();
		public IEnumerable<Folder> GetUserFolders(int userId, int? parentId = null);
		public Folder GetSpecificFolder(int id);
		public void AddFolder(string name, int parentId);
		public void DeleteFolder(Folder folder);
		public void UpdateFolder(Folder folder);
	}
}
