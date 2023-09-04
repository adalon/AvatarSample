using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvatarSample
{
    /// <summary>
    /// Caches an avatar entry
    /// </summary>
    public interface IAvatarCache
    {
        bool TryGet(string email, [NotNullWhen(true)] out string avatar);

        void Add(string email, string avatar);
    }
}