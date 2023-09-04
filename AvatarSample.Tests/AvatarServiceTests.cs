using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvatarSample.Tests
{
    public class AvatarServiceTests
    {
        [Fact]
        public void AvatarService_DownloadAvatar_FromEmail()
        {
            var service = CreateService(out var provider, out _);

            Mock.Get(provider)
                .Setup(x => x.DownloadAvatar("foo@github.com"))
                .Returns("foo_avatar");

            var avatar = service.GetAvatar("foo@github.com");

            Assert.Equal("foo_avatar", avatar);
        }

        [Fact]
        public void AvatarService_DownloadAvatar_FromCommitAuthor()
        {
            var service = CreateService(out var provider, out _);

            var commit = new Commit("123", "bar@github.com", "baz@github.com");

            Mock.Get(provider)
                .Setup(x => x.DownloadAvatar(commit.AuthorEmail, commit.Sha))
                .Returns("bar_avatar");

            var avatar = service.GetAvatar(commit, useCommitter: false);

            Assert.Equal("bar_avatar", avatar);
        }

        [Fact]
        public void AvatarService_DownloadAvatar_FromCommitCommitter()
        {
            var service = CreateService(out var provider, out _);

            var commit = new Commit("123", "bar@github.com", "baz@github.com");

            Mock.Get(provider)
                .Setup(x => x.DownloadAvatar(commit.CommitterEmail, commit.Sha))
                .Returns("baz_avatar");

            var avatar = service.GetAvatar(commit, useCommitter: true);

            Assert.Equal("baz_avatar", avatar);
        }

        private AvatarService CreateService(out IAvatarProvider avatarProvider, out IAvatarCache cache)
        {
            avatarProvider = Mock.Of<IAvatarProvider>();
            cache = Mock.Of<IAvatarCache>();

            return new AvatarService(avatarProvider, cache);
        }
    }
}