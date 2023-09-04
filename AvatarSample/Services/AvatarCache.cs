using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvatarSample
{
    public class AvatarCache : IAvatarCache
    {
        private readonly ConcurrentDictionary<string, string> avatars = new();

        public void Add(string email, string avatar) =>
            avatars[email] = avatar;

        public void Clear() =>
            avatars.Clear();

        public bool TryGet(string email, [NotNullWhen(true)] out string avatar) =>
            avatars.TryGetValue(email, out avatar);
    }
}