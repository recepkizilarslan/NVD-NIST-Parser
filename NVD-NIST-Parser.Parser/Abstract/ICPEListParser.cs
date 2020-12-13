using NVDParser.Parser.Entities;
using System.Collections.Generic;

namespace NVDParser.Parser.Abstract
{
    public interface ICPEListParser
    {
        List<CPE> ParseCPEOnDOM(string uri);
    }
}
