using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvatarSample
{
    public class AvatarService : IAvatarService
    {
        private readonly IAvatarProvider avatarProvider;
        private readonly IAvatarCache cache;

        public AvatarService(IAvatarProvider avatarProvider, IAvatarCache cache)
        {
            this.avatarProvider = avatarProvider;
            this.cache = cache;
        }

        public string GetAvatar(string email, bool useCache = true)
        {
            // TODO: Refactor shared logic with GetAvatar (from commit)

            if (useCache && cache.TryGet(email, out var avatar))
            {
                return avatar;
            }

            avatar = avatarProvider.DownloadAvatar(email);

            if (useCache)
            {
                cache.Add(email, avatar);
            }

            return avatar;
        }

        public string GetAvatar(Commit commit, bool useCommitter, bool useCache = true)
        {
            // TODO: Refactor shared logic with GetAvatar (from email)

            var email = useCommitter ? commit.CommitterEmail : commit.AuthorEmail;

            if (useCache && cache.TryGet(email, out var avatar))
            {
                return avatar;
            }

            avatar = avatarProvider.DownloadAvatar(email, commit.Sha);

            if (useCache)
            {
                cache.Add(email, avatar);
            }

            return avatar;
        }
    }
}