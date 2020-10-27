using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using PingPing.GithubParser.Abstract;
using PingPing.GithubParser.Entities;

namespace PingPing.GithubParser.Concrete
{
    public class GithubVersionChecker : IGithubVersionChecker
    {
        private static readonly HtmlWeb _web = new HtmlWeb();

        private List<string> GetReleasesPage(string profile, string repo, string lastVersion = null)
        {
            var result = new List<string>();

            var releasesUrl = $"https://github.com/{profile}/{repo}/tags";

            if (!string.IsNullOrEmpty(lastVersion))
            {
                releasesUrl += $"?after={lastVersion}";
            }

            var doc = _web.Load(releasesUrl);

            var versionNodes = doc.DocumentNode.SelectNodes("//h4[contains(@class, 'commit-title')]/a");

            if (versionNodes == null)
            {
                return result;
            }

            foreach (var node in versionNodes)
            {
                result.Add(node.InnerText.Trim());
            }

            return result;
        }

        /// <summary>
        /// This class provides collect versions on github api
        /// </summary>
        /// <param name="Result"></param>
        /// <returns>VersionList</returns>
        public List<GithubVersions> CollectVersions(string url)
        {
            var result = new List<GithubVersions>();

            if(!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uri))
            {
                return result;
            }

            var urlSplit = uri.PathAndQuery.Split("/");

            if(urlSplit.Length != 3)
            {
                return result;
            }

            var profile = urlSplit[1];
            var repo = urlSplit[2];

            var versions = GetReleasesPage(profile, repo);

            result.AddRange(versions.Select(v => new GithubVersions { Name = v }));

            while (versions.Count > 0)
            {
                versions = GetReleasesPage(profile, repo, result.Last().Name);

                if (versions.Count == 0)
                {
                    break;
                }

                result.AddRange(versions.Select(v => new GithubVersions { Name = v }));
            }

            return result;
         }
           
    }
}
