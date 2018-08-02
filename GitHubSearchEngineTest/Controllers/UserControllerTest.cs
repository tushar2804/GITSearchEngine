using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using GitHubSearch.Interface;
using GitHubSearch.Models;
using GitHubSearch.Controllers;
using Moq;
using System.Web.Mvc;

namespace GitHubSearchEngineTest.Controllers
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class UserControllerTest
    {
        private Mock<IRepoDetailsService> _repoDetailsServiceMock;
        private Mock<IUserDetailsService> _userDetailsServiceMock;
        private UserController _userController;

        [SetUp]
        public void SetUp()
        {
            _userDetailsServiceMock = new Mock<IUserDetailsService>();
            _repoDetailsServiceMock = new Mock<IRepoDetailsService>();
            _userDetailsServiceMock.Setup(x => x.GetUserDetails(It.IsAny<string>())).Returns(It.IsAny<UserDetailsModel>);
            _repoDetailsServiceMock.Setup(x => x.GetUserRepos(It.IsAny<UserDetailsModel>())).Returns(It.IsAny<UserDetailsModel>);
            _userController = new UserController(_repoDetailsServiceMock.Object, _userDetailsServiceMock.Object);
        }

        [Test]
        public void If_ModelState_Not_Valid_Return_User_Status_Not_Valid()
        {
              var userDetailsModel = new UserDetailsModel()
              {
                  Username = "jyotishma370$%$%"
              };

            var response = _userController.User(userDetailsModel) as ViewResult;
            var data = (UserDetailsModel)response?.ViewData.Model;
            Assert.AreEqual("Model is not valid",data?.status);
        }

        [Test]
        public void If_Response_From_User_Service_Call_Is_Not_Null_Then_Bind_View_With_Model_Data()
        {
            var userDetailsModel = new UserDetailsModel()
            {
                Username = "jyotishma370"
            };

            var response = _userController.User(userDetailsModel) as ViewResult;
            var data = (UserDetailsModel)response?.ViewData.Model;
            Assert.AreEqual("Success", data?.status);
        }

        [Test]
        public void If_Response_From_User_Service_Call_Is_Null_Then_Return_User_Status_User_Not_Available()
        {
            var userDetailsModel = new UserDetailsModel()
            {
                Username = "j34gh"
            };

            var response = _userController.User(userDetailsModel) as ViewResult;
            var data = (UserDetailsModel)response?.ViewData.Model;
            Assert.AreEqual("The user is not available in the hub", data?.status);
        }
    }
}
