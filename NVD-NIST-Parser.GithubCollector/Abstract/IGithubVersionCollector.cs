using System.Collections.Generic;
using PingPing.GithubParser.Entities;

namespace PingPing.GithubParser.Abstract
{
    public interface IGithubVersionChecker
    {
        List<GithubVersions> CollectVersions(string url);
    }
}
