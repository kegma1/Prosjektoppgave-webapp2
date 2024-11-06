

namespace prosjekt_webapp2.Data.Repositories {
	public interface IFolderRepository {
		public IEnumerable<Folder> GetAllFolders();
		public IEnumerable<Folder> GetUserFolders(int userId, int? parentId = null);
	}
}
