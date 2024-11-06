using Microsoft.EntityFrameworkCore;

namespace prosjekt_webapp2.Data.Repositories {
	public class FolderRepository : IFolderRepository {
		private readonly AppDbContext _context;

		public FolderRepository(AppDbContext context) {
			_context = context;
		}

		public IEnumerable<Folder> GetAllFolders() {
			return _context.Folder.Include(b => b.ParentFolder).ToList();
		}

		public IEnumerable<Folder> GetUserFolders(int userId, int? parentId = null) {
			return _context.Folder.Where(b => b.UserId == userId).Where(b => b.ParentFolderId == parentId).Include(b => b.ParentFolder).Include(b => b.Owner).ToList();
		}
	}
}
