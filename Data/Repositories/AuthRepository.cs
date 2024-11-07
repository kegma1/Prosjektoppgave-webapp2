using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace prosjekt_webapp2.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            if (await _context.User.AnyAsync(u => u.Username == user.Username))
            {
                return false;
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> LoginUserAsync(string username, string password)
        {
            return await _context.User
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

		public async Task<bool> DeleteUserAsync(string username)
		{
    		var user = await _context.User.FirstOrDefaultAsync(u => u.Username == username);
    		if (user == null)
    		{
        		return false;
    		}

    		_context.User.Remove(user);
    		await _context.SaveChangesAsync();
    		return true;
		}

    }
}
