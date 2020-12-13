using System.Collections.Generic;

namespace NVDParser.Parser.Abstract
{
    public interface ICVEListParser
    {
        List<string> ParsePage(string url);
    }
}
