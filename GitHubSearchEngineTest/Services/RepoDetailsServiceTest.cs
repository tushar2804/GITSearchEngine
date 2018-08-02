using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using GitHubSearch.Interface;
using GitHubSearch.Models;
using System.Collections.Generic;
using GitHubSearch.Library;
using Moq;

namespace GitHubSearchEngineTest.Services
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class RepoDetailsServiceTest
    {
        private RepoDetailsService _repoDetailsService;
        private Mock<UserDetailsModel> _userDetailsModelMock;
        private Mock<UserRepoDetailsModel> _userRepoDetailsModelMock;


        [SetUp]
        public void SetUp()
        {
            _userDetailsModelMock = new Mock<UserDetailsModel>();
            _userRepoDetailsModelMock = new Mock<UserRepoDetailsModel>();
            _repoDetailsService = new RepoDetailsService(_userDetailsModelMock.Object,_userRepoDetailsModelMock.Object);
        }

        [Test]
        public void GetUserRepoDetails_Return_APiResponse_Success_When_No_Error()
        {
            var response = _repoDetailsService.GetUserRepos(PassModel());
            Assert.AreEqual(ExpectedModel(), response);
        }

        [Test]
        public void GetUserRepoDetails_Return_APiResponse_Error_When_response_Is_Null()
        {
            var response = _repoDetailsService.GetUserRepos(null);
            Assert.AreEqual("API response is null", response.status);
        }

        public UserDetailsModel PassModel()
        {
            return new UserDetailsModel()
            {
                Username = "jyotisharma370",
                Name = null,
                Location = null,
                Avtar = "https://avatars0.githubusercontent.com/u/30447666?v=4"
            };
        }

        public UserDetailsModel ExpectedModel()
        {
            return new UserDetailsModel()
            {
                Username = "jyotisharma370",
                Name = null,
                Location = null,
                Avtar = "https://avatars0.githubusercontent.com/u/30447666?v=4",
                RepoDetails = new List<UserRepoDetailsModel>()
                {
                    new UserRepoDetailsModel()
                    {
                        Id = 98466297,
                        RepoName = "BGLAssignment",
                        Url = "https://api.github.com/users/jyotisharma370/BGLAssignment",
                        HtmlUrl = "https://github.com/jyotisharma370/BGLAssignment",
                        Description = "",
                        StargazersCount=0
                    },
                     new UserRepoDetailsModel()
                    {
                        Id = 143151937,
                        RepoName = "new-assignment",
                        Url = "https://api.github.com/users/jyotisharma370/new-assignment",
                        HtmlUrl = "https://github.com/jyotisharma370/new-assignment",
                        Description = "",
                        StargazersCount=0
                    }
                }

            };
        }
    }
}
