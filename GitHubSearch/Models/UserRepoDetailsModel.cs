using System.Diagnostics.CodeAnalysis;

namespace GitHubSearch.Models
{
    [ExcludeFromCodeCoverage]
    public class UserRepoDetailsModel
    {
        public string RepoName { get; set; }

        public string Url { get; set; }

        public int Id { get; set; }

        public int StargazersCount { get; set; }

        public string HtmlUrl { get; set; }

        public string Description { get; set; }
    }
}
