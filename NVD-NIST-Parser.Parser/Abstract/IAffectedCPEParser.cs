using NVDParser.Parser.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NVDParser.Parser.Abstract
{
    public interface IAffectedCPEParser
    {
        List<string> ParseCPEMap(string json);
        Task<CPEResult> GetCPE(string uri);
        List<string> ParseCPE(CPEResult cpeResult, string vendorName, string productName);
    }
}
