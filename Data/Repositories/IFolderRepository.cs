﻿

namespace prosjekt_webapp2.Data.Repositories {
	public interface IFolderRepository {
		public IEnumerable<Folder> GetAllFolders();
		public IEnumerable<Folder> GetUserFolders(int userId);
		public Folder GetSpecificFolder(int id);
		public Folder AddFolder(Folder folder);
		public void DeleteFolder(Folder folder);
		public Folder UpdateFolder(Folder folder);
		public IEnumerable<Folder> GetSubFolders(Folder folder);
	}
}
