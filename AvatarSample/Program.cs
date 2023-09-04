// See https://aka.ms/new-console-template for more information

using AvatarSample;

var cache = new AvatarCache();
var avatarProvider = new GitHubAvatarProvider();
var avatarService = new AvatarService(avatarProvider, cache);

var userAvatar = avatarService.GetAvatar("foo@github.com");
Console.WriteLine(userAvatar);

var commit = new Commit("123", "bar@github.com", "baz@github.com");
var authorAvatar = avatarService.GetAvatar(commit, useCommitter: false);
Console.WriteLine(authorAvatar);

var committerAvatar = avatarService.GetAvatar(commit, useCommitter: true);
Console.WriteLine(committerAvatar);