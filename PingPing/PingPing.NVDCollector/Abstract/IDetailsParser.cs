using PingPing.NVDParser.Entities;

namespace PingPing.NVDParser.Abstract
{
    public interface IDetailsParser
    {
        NVDVulnerability ParseDetails(string cve);
    }
}
