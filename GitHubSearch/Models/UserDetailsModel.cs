using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GitHubSearch.Models
{
    [ExcludeFromCodeCoverage]
    public class UserDetailsModel
    {
        [Required(ErrorMessage = "Required")] public string Username { get; set; }

        public string Name { get; set; }

        public string status { get; set; }

        [DisplayFormat(NullDisplayText = "NULL")]
        public string Location { get; set; }

        public string Avtar { get; set; }

        public List<UserRepoDetailsModel> RepoDetails { get; set; }
    }
}
