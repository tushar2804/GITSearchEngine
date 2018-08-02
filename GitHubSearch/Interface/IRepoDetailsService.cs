using GitHubSearch.Models;

namespace GitHubSearch.Interface
{
    public interface IRepoDetailsService
   {
        UserDetailsModel GetUserRepos(UserDetailsModel user);
   }
}
