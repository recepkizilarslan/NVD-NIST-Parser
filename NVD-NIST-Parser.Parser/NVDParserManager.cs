using NVDParser.Core;
using NVDParser.Parser.Abstract;
using NVDParser.Parser.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NVDParser.Parser
{
    /// <summary>
    /// This class provides to manage to NVD parsing procces.
    /// </summary>
    public class NVDParserManager:INVDParserManager
    {  
        //we use NVDCVECollector class for collecting all cve number. 
        private readonly ICVEListParser _cveParser;

        private readonly ICPEListParser _cpeListParser;
        
        //we use NVDDetailsCollector class for collecting cve details.
        private readonly IDetailsParser _detailsParser;

        //we use NVDVersionCollector  class for parsing CPE versions
        private readonly IAffectedCPEParser _cpeParser;

        public NVDParserManager(ICVEListParser cveParser,
                                IDetailsParser detailsParser, 
                                IAffectedCPEParser cpeParser,
                                ICPEListParser cpeListParser
                                )
        {
            _cveParser = cveParser;
            _detailsParser = detailsParser;
            _cpeParser = cpeParser;
            _cpeListParser = cpeListParser;
        }

        /// <summary>
        /// This function provides to crawl all cve of product
        /// </summary>
        /// <param name="vendorName"></param>
        /// <param name="productName"></param>
        /// <returns>Queue<string> Crawled CVE List</returns>
        public List<string> ParseAllCVE(string vendorName,string productName)
        {
            string url = $"https://nvd.nist.gov/vuln/search/results?cpe_vendor=cpe:/:{vendorName}&cpe_product=cpe:/:{vendorName}:{productName}";
            return _cveParser.ParsePage(url);
        }
        
        /// <summary>
        /// This method provides parse details of releated cve
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<Vulnerability> ParseDetails(string cve, string vendorName, string productName)
        {
            Vulnerability vulnerability = new Vulnerability();
            NVDVulnerability nvdVulnerability= _detailsParser.ParseDetails(cve);
            
            vulnerability.Vendor = vendorName;
            vulnerability.Product = productName;
            vulnerability.Refers = nvdVulnerability.Refers;
            vulnerability.Title = nvdVulnerability.Title;
            vulnerability.Overview = nvdVulnerability.Overview;
            vulnerability.CvssMetric = nvdVulnerability.CvssMetric;
            vulnerability.Severity = nvdVulnerability.Severtiy;
            vulnerability.AffectedVersions = await ParseAffectedCPE(nvdVulnerability.CPEMapJSON, vendorName, productName);

            return vulnerability;
           
        }
            
           

        /// <summary>
        /// this function provides to parsing all versions of vulnerability
        /// </summary>
        /// <param name="json"></param>
        /// <returns>All Affected Versions</returns>
        private async Task<List<string>> ParseAffectedCPE(string cpeMapJson, string productName, string vendorName)
        {
            List<string> cpes = new List<string>();
            //get cpe(s) url
            List<string> cpeMapURLS = _cpeParser.ParseCPEMap(cpeMapJson);

            foreach (var uri in cpeMapURLS)
            {
                CPEResult cpeResult = await _cpeParser.GetCPE(uri);

                cpes.AddRange(_cpeParser.ParseCPE(cpeResult, vendorName, productName));
                  
            }
            //return list
            return cpes;
        }

        /// <summary>
        /// This method provides to parse all versions on the DOM
        /// </summary>
        /// <param name="targetURL"></param>
        /// <returns>List<CPE></returns>
        public List<string> ParseAllCPE(string vendorName, string productName)
        {
            List<string> a = new List<string>();
            //string url = $"https://nvd.nist.gov/products/cpe/search/results?namingFormat=2.3&keyword=cpe:/:{vendorName}:{productName}";
            //return _cpeListParser.ParseCPEOnDOM(url);

            return a ;
        }


  
    }
}
