using System.Web.Mvc;
using GitHubSearch.Interface;
using GitHubSearch.Library;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector;

namespace GitHubSearch
{
    public class DependencyRegistration
    {
        public static void RegisterComponents()
        {
            var container = new Container();
            container.Register<IRepoDetailsService, RepoDetailsService>();
            container.Register<IUserDetailsService, UserDetailsService>();
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }

}
