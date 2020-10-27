using System.Collections.Generic;

namespace PingPing.NVDParser.Abstract
{
    public interface ICVEListParser
    {
        List<string> ParsePage(string url);
    }
}
