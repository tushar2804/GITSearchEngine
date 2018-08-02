using GitHubSearch.Interface;
using GitHubSearch.Models;
using Newtonsoft.Json;
using ServiceStack;

namespace GitHubSearch.Library
{
    public class UserDetailsService : IUserDetailsService
    {
        private readonly UserDetailsModel _userDetailsModel;

        public UserDetailsService(UserDetailsModel userDetailsModel)
        {
            _userDetailsModel = userDetailsModel;
        }

        public UserDetailsModel GetUserDetails(string username)
        {
            string apiResponse = "";
            System.Net.ServicePointManager.Expect100Continue = true;
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            string url = "https://api.github.com/users/" + username.Trim();
            var client = new JsonServiceClient(url);
            try
            {
                apiResponse = client.Get<string>("");
                dynamic dynObj = JsonConvert.DeserializeObject(apiResponse);
                _userDetailsModel.Username = username;
                _userDetailsModel.Name = (string)dynObj["name"];
                _userDetailsModel.Location = (string)dynObj["location"];
                _userDetailsModel.Avtar = (string)dynObj["avatar_url"];
                _userDetailsModel.status = "Success";
            }
            catch (WebServiceException e)
            {
                _userDetailsModel.status = "Error";
                apiResponse = e.Message;
            }
            return _userDetailsModel;
        }
    }
}

