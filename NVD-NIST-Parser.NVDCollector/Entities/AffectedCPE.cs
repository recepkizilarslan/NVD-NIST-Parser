using System.Collections.Generic;

namespace PingPing.NVDParser.Entities
{
    public class AffecteCPE
    {
        public int id { get; set; }
        public List<Container> containers { get; set; }
    }

    public class Container
    {
        public List<CpeIdNode> cpes { get; set; }
    }

    public class CpeIdNode
    {
        public string id { get; set; }
    }



    public class CPEResult
    {
        public Cpes cpes { get; set; }
    }
    public class Cpes
    {
        public List<CPE> cpes { get; set; }
    }

    public class CPE
    {
        public string vendor { get; set; }
        public string product { get; set; }
        public string version { get; set; }
        public object update { get; set; }
        public object edition { get; set; }
        public object language { get; set; }
    }


}
