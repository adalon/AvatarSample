using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvatarSample
{
    /// <summary>
    /// Responsible for getting avatars from different sources such as emails, commits, etc
    /// with some level of caching.
    /// </summary>
    public interface IAvatarService
    {
        string GetAvatar(string email, bool useCache = true);

        string GetAvatar(Commit commit, bool useCommitter, bool useCache = true);
    }
}