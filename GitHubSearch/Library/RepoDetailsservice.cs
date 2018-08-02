using System.Collections.Generic;
using System.Linq;
using GitHubSearch.Interface;
using GitHubSearch.Models;
using Newtonsoft.Json.Linq;
using ServiceStack;

namespace GitHubSearch.Library
{
    public class RepoDetailsService : IRepoDetailsService
    {
        private readonly UserDetailsModel _userDetailsModel;
        private UserRepoDetailsModel _userRepoDetailsModel;

        public RepoDetailsService(UserDetailsModel userDetailsModel, UserRepoDetailsModel userRepoDetailsModel)
        {
            _userDetailsModel = userDetailsModel;
            _userRepoDetailsModel = userRepoDetailsModel;
        }

        public UserDetailsModel GetUserRepos(UserDetailsModel user)
        {
            string userRepos = "";
            System.Net.ServicePointManager.Expect100Continue = true;
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            string url = "https://api.github.com/users/" + user.Username.Trim() + "/repos";
            var client = new JsonServiceClient(url);
            _userDetailsModel.RepoDetails = new List<UserRepoDetailsModel>();
            try
            {
                userRepos = client.Get<string>("");
                if (userRepos != null)
                {
                    JArray arrayOfUserRepose = JArray.Parse(userRepos);
                    JArray sorted = new JArray(arrayOfUserRepose.OrderByDescending(obj => obj["stargazers_count"]));
                    int counter = 0;

                    foreach (var item in sorted.Children())
                    {
                        var itemProperties = item.Children<JProperty>();
                        var myRepoId = itemProperties.FirstOrDefault(x => x.Name == "id");
                        var myRepoName = itemProperties.FirstOrDefault(x => x.Name == "name");
                        var myRepoURL = itemProperties.FirstOrDefault(x => x.Name == "url");
                        var myHtmlURL = itemProperties.FirstOrDefault(x => x.Name == "html_url");
                        var myDescription = itemProperties.FirstOrDefault(x => x.Name == "description");
                        var mystargazers_count = itemProperties.FirstOrDefault(x => x.Name == "stargazers_count");

                        _userDetailsModel.Username = user.Username;
                        _userDetailsModel.Name = user.Name; 
                        _userDetailsModel.Location = user.Location; 
                        _userDetailsModel.Avtar = user.Avtar;

                        _userRepoDetailsModel = new UserRepoDetailsModel();

                        _userRepoDetailsModel.Id = myRepoId.Value.ToObject<int>();
                        _userRepoDetailsModel.RepoName = myRepoName.ToObject<string>();
                        _userRepoDetailsModel.Url = myRepoURL.ToObject<string>();
                        _userRepoDetailsModel.HtmlUrl = myHtmlURL.ToObject<string>();
                        _userRepoDetailsModel.Description = myDescription.ToObject<string>();
                        _userRepoDetailsModel.StargazersCount = mystargazers_count.ToObject<int>();

                        _userDetailsModel.RepoDetails.Add(_userRepoDetailsModel);

                        counter++;
                        if (counter == 5) break;
                    }

                }
                else
                    _userDetailsModel.status = "API response is null";
            }
            catch (WebServiceException e)
            {
                throw e;
            }

            return _userDetailsModel;
        }
    }
}
