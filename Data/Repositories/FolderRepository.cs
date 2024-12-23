﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace prosjekt_webapp2.Data.Repositories {
	public class FolderRepository : IFolderRepository {
		private readonly AppDbContext _context;

		public FolderRepository(AppDbContext context) {
			_context = context;
		}

		public IEnumerable<Folder> GetAllFolders() {
			return _context.Folder.Include(b => b.ParentFolder).ToList();
		}

		public IEnumerable<Folder> GetUserFolders(int userId) {
			return _context.Folder
				.Where(b => b.UserId == userId)
				.Include(b => b.ParentFolder)
				.Include(b => b.Owner)
				.ToList();
		}
		public Folder GetSpecificFolder(int id) {
			return _context.Folder
				.Include(b => b.ParentFolder)
				.Include(b => b.Owner)
				.FirstOrDefault(b => b.Id == id);
		}

		public Folder AddFolder(Folder folder) {
			_context.Folder.Add(folder);
			_context.SaveChanges();
			return folder;
		}

		public void DeleteFolder(Folder folder) {
			_context.Folder.Remove(folder);
			_context.SaveChanges();
		}

		public Folder UpdateFolder(Folder folder) {
			_context.Folder.Update(folder);
			_context.SaveChanges();
			return folder;
		}
		
		public IEnumerable<Folder> GetSubFolders(Folder folder) {
			return _context.Folder
				.Where(b => b.ParentFolderId == folder.Id)
				.Include(b => b.ParentFolder)
				.Include(b => b.Owner)
				.ToList();
		}
	}
}
