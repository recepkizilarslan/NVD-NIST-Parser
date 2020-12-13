using HtmlAgilityPack;
using NVDParser.Parser.Abstract;
using NVDParser.Parser.Entities;

namespace NVDParser.Parser.Concrete
{
    public class DetailsParser : IDetailsParser
    {
        //use agalitypack web object
        HtmlWeb web = new HtmlWeb();
        
        /// <summary>
        /// This function provides to parse cve details page main information
        /// </summary>
        /// <param name="cve"></param>
        /// <returns></returns>
        public NVDVulnerability ParseDetails(string cve)
        {

            NVDVulnerability vulnerability = new NVDVulnerability();
            
            //load details page
            var DetailsPage = web.Load($"https://nvd.nist.gov/vuln/detail/{cve}");

            //Get Refers
            var Refers = DetailsPage.DocumentNode.SelectSingleNode("//h2/span[1]");

            //Get CVSS Metric
            var CvssMetric= DetailsPage.DocumentNode.SelectSingleNode("//*[@id=\"Vuln3CvssPanel\"]/div[1]/div[3]/span/span");

            //Get CVSS Score
            var CVSS = DetailsPage.DocumentNode.SelectSingleNode("//*[@id=\"Cvss3NistCalculatorAnchor\"]");
        
            //Get Title
            var Title = DetailsPage.DocumentNode.SelectSingleNode("//*[@id=\"vulnTechnicalDetailsDiv\"]/table/tbody/tr/td[2]");

            //Get overview table
            var Overview = DetailsPage.DocumentNode.SelectSingleNode("//div[@class='col-lg-9 col-md-7 col-sm-12']/p[1]");

            //Get CPEMapJson
            var CPEMapJSON = DetailsPage.DocumentNode.SelectSingleNode("//*[@id=\"cveTreeJsonDataHidden\"]");

            vulnerability.Refers = Refers.InnerText;
            vulnerability.Severtiy = CVSS.InnerText;
            vulnerability.CvssMetric = CvssMetric.InnerText;
            vulnerability.Overview = Overview.InnerText;
            vulnerability.CvssMetric = CvssMetric.InnerText;
            vulnerability.Title = System.Web.HttpUtility.HtmlDecode(Title.InnerText);
            vulnerability.CPEMapJSON = CPEMapJSON.Attributes["value"].Value;

            return vulnerability;
        }

    }
}

