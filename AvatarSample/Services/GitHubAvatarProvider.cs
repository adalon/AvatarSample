using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvatarSample
{
    public class GitHubAvatarProvider : IAvatarProvider
    {
        public string DownloadAvatar(string email)
        {
            // Simulate some avatar downloading from github
            return email + "_github_avatar";
        }

        public string DownloadAvatar(string email, string commitSha)
        {
            // Simulate some avatar downloading from github
            return email + commitSha + "_github_avatar";
        }
    }
}