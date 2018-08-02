using GitHubSearch.Models;

namespace GitHubSearch.Interface
{
    public interface IUserDetailsService
   {
        UserDetailsModel GetUserDetails(string username);
   }
}
