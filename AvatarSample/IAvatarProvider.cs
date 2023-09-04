using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvatarSample
{
    /// <summary>
    /// Download an avatar from a given backend. E.g. azdo, github, etc
    /// </summary>
    public interface IAvatarProvider
    {
        string DownloadAvatar(string email);

        string DownloadAvatar(string email, string commitSha);
    }
}