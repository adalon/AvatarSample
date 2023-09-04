using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvatarSample
{
    public class Commit
    {
        public Commit(string sha, string authorEmail, string committerEmail)
        {
            Sha = sha;
            AuthorEmail = authorEmail;
            CommitterEmail = committerEmail;
        }

        public string Sha { get; }

        public string AuthorEmail { get; }

        public string CommitterEmail { get; }
    }
}