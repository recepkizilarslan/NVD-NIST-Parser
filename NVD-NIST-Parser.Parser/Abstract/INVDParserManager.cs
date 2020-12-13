using NVDParser.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NVDParser.Parser.Abstract
{
    public  interface INVDParserManager
    {
        List<string> ParseAllCVE(string vendorName, string productName);
        List<string> ParseAllCPE(string vendorName, string productName);
        Task<Vulnerability> ParseDetails(string cve, string vendorName, string productName);
    }
}
