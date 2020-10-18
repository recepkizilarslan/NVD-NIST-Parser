using PingPing.NVDParser.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PingPing.NVDParser.Abstract
{
    public interface IAffectedCPEParser
    {
        List<string> ParseCPEMap(string json);
        Task<CPEResult> GetCPE(string uri);
        List<string> ParseCPE(CPEResult cpeResult, string vendorName, string productName);
    }
}
