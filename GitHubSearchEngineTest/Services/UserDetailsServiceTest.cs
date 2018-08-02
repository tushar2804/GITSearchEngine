using System.Diagnostics.CodeAnalysis;
using GitHubSearch.Interface;
using GitHubSearch.Library;
using GitHubSearch.Models;
using Moq;
using NUnit.Framework;

namespace GitHubSearchEngineTest.Services
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class UserDetailsServiceTest
    {
        private UserDetailsService _userDetailsService;
        private Mock<UserDetailsModel> _userDetailsModelMock;

        [SetUp]
        public void SetUp()
        {
            _userDetailsModelMock = new Mock<UserDetailsModel>();
            _userDetailsService = new UserDetailsService(_userDetailsModelMock.Object);
        }

        [Test]
        public void GetUserDetails_Return_APiResponse_Success_When_No_Error()
        {
            var response = _userDetailsService.GetUserDetails(PassModel().Name);
            Assert.AreEqual("Success", response.status);
        }

        [Test]
        public void GetUserDetails_Return_APiResponse_Error_When_response_Is_Null()
        {
            var response = _userDetailsService.GetUserDetails("XXXX");
            Assert.AreEqual("Error", response.status);
        }

        public UserDetailsModel PassModel()
        {
            return new UserDetailsModel()
            {
                Username = "jyotisharma370"
          };
        }

        public UserDetailsModel ExpectedModel()
        {
            return new UserDetailsModel()
            {
                Username = "jyotisharma370",
                Name = null,
                Location = null,
                Avtar = "https://avatars0.githubusercontent.com/u/30447666?v=4"
            };
        }
    }
}
