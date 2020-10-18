using PingPing.NVDParser.Entities;
using System.Collections.Generic;

namespace PingPing.NVDCollector.Abstract
{
    public interface ICPEListParser
    {
        List<CPE> ParseCPEOnDOM(string uri);
    }
}
