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
            return GetAvatarCore(
                email,
                provider => provider.DownloadAvatar(email),
                useCache);
        }

        public string GetAvatar(Commit commit, bool useCommitter, bool useCache = true)
        {
            var email = useCommitter ? commit.CommitterEmail : commit.AuthorEmail;

            return GetAvatarCore(
                email,
                provider => provider.DownloadAvatar(email, commit.Sha),
                useCache);
        }

        private string GetAvatarCore(string email, Func<IAvatarProvider, string> avatarProviderCallback, bool useCache = true)
        {
            if (useCache && cache.TryGet(email, out var avatar))
            {
                return avatar;
            }

            avatar = avatarProviderCallback(avatarProvider);

            if (useCache)
            {
                cache.Add(email, avatar);
            }

            return avatar;
        }
    }
}