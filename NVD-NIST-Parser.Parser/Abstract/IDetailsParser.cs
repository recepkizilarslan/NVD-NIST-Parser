

using NVDParser.Parser.Entities;

namespace NVDParser.Parser.Abstract
{
    public interface IDetailsParser
    {
        NVDVulnerability ParseDetails(string cve);
    }
}
