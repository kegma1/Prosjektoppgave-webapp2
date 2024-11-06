namespace prosjekt_webapp2.Data.Repositories {
	public class AuthRepository : IAuthRepository {
		private readonly AppDbContext _context;

		public AuthRepository(AppDbContext context) {
			_context = context;
		}


	}
}
