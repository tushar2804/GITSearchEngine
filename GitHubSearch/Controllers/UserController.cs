using System.Web.Mvc;
using GitHubSearch.Interface;
using GitHubSearch.Models;
namespace GitHubSearch.Controllers
{

    public class UserController : Controller
    {   
        private readonly IRepoDetailsService _repoUser;
        private readonly IUserDetailsService _user;

        public UserController(IRepoDetailsService repoUser,IUserDetailsService user)
        {
            this._repoUser = repoUser;
            this._user = user;
        }

        // GET: User
        [HttpGet]
        public new ActionResult User()
        {
           return View();
        }

        [HttpPost]
        public new ActionResult User(GitHubSearch.Models.UserDetailsModel userModel)
        {
            var user = new UserDetailsModel();
            if (ModelState.IsValid)
            {
                user = _user.GetUserDetails(userModel.Username);

                if (user != null)
                {
                    ViewBag.errMsg = "";
                    user = _repoUser.GetUserRepos(user);
                    user.status = "Success";
                    return View(user);
                }
                else
                {
                    ViewBag.errMsg = "The user is not available in the hub";
                    return View(user.status = "The user is not available in the hub");
                }
                
            }
            return View(user.status = "Model is not valid");
        }
       
    }
}