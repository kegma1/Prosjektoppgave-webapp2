using System.Threading.Tasks;

namespace prosjekt_webapp2.Data.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> RegisterUserAsync(User user);
        Task<User?> LoginUserAsync(string username, string password);

		Task<bool> DeleteUserAsync(string username);

    }
}
