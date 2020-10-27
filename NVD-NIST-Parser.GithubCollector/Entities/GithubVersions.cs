using System.Text.Json.Serialization;

namespace PingPing.GithubParser.Entities
{
    public class GithubVersions
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("zipball_url")]
        public string DownloadLink { get; set; }
    }
}
