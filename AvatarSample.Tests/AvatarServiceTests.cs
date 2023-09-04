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
            var service = CreateService(out var provider, out var cache);

            Mock.Get(provider)
                .Setup(x => x.DownloadAvatar("foo@github.com"))
                .Returns("foo_avatar");

            var avatar = service.GetAvatar("foo@github.com");

            Assert.Equal("foo_avatar", avatar);

            // And verify the cache was updated
            Mock.Get(cache)
                .Verify(x => x.Add("foo@github.com", "foo_avatar"), Times.Once);
        }

        [Fact]
        public void AvatarService_DownloadAvatar_FromCommitAuthor()
        {
            var service = CreateService(out var provider, out var cache);

            var commit = new Commit("123", "bar@github.com", "baz@github.com");

            Mock.Get(provider)
                .Setup(x => x.DownloadAvatar(commit.AuthorEmail, commit.Sha))
                .Returns("bar_avatar");

            var avatar = service.GetAvatar(commit, useCommitter: false);

            Assert.Equal("bar_avatar", avatar);

            // And verify the cache was updated
            Mock.Get(cache)
                .Verify(x => x.Add("bar@github.com", "bar_avatar"), Times.Once);
        }

        [Fact]
        public void AvatarService_DownloadAvatar_FromCommitCommitter()
        {
            var service = CreateService(out var provider, out var cache);

            var commit = new Commit("123", "bar@github.com", "baz@github.com");

            Mock.Get(provider)
                .Setup(x => x.DownloadAvatar(commit.CommitterEmail, commit.Sha))
                .Returns("baz_avatar");

            var avatar = service.GetAvatar(commit, useCommitter: true);

            Assert.Equal("baz_avatar", avatar);

            // And verify the cache was updated
            Mock.Get(cache)
                .Verify(x => x.Add("baz@github.com", "baz_avatar"), Times.Once);
        }

        [Fact]
        public void AvatarService_GetAvatar_FromEmailUsingCache()
        {
            var service = CreateService(out var provider, out var cache);

            // Setup cached avatar
            string cachedAvatar = "foo_avatar";
            Mock.Get(cache)
                .Setup(x => x.TryGet("foo@github.com", out cachedAvatar))
                .Returns(true);

            // Get the avatar from the cache without downloading
            var avatar = service.GetAvatar("foo@github.com");

            Assert.Equal("foo_avatar", avatar);

            // And verify Download was never invoked
            Mock.Get(provider)
                .Verify(x => x.DownloadAvatar("foo@github.com"), Times.Never);

            // And the cache was not updated
            Mock.Get(cache)
                .Verify(x => x.Add("foo@github.com", It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void AvatarService_GetAvatar_FromCommitUsingCache()
        {
            var service = CreateService(out var provider, out var cache);

            // Setup cached avatar
            string cachedAvatar = "bar_avatar";
            Mock.Get(cache)
                .Setup(x => x.TryGet("bar@github.com", out cachedAvatar))
                .Returns(true);

            var commit = new Commit("123", "bar@github.com", "baz@github.com");

            // Get the avatar from the cache without downloading
            var avatar = service.GetAvatar(commit, useCommitter: false);

            Assert.Equal("bar_avatar", avatar);

            // And verify Download was never invoked
            Mock.Get(provider)
                .Verify(x => x.DownloadAvatar("bar@github.com"), Times.Never);

            // And the cache was not updated
            Mock.Get(cache)
                .Verify(x => x.Add("bar@github.com", It.IsAny<string>()), Times.Never);
        }

        private AvatarService CreateService(out IAvatarProvider avatarProvider, out IAvatarCache cache)
        {
            avatarProvider = Mock.Of<IAvatarProvider>();
            cache = Mock.Of<IAvatarCache>();

            return new AvatarService(avatarProvider, cache);
        }
    }
}