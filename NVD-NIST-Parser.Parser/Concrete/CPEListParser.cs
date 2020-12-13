using HtmlAgilityPack;
using NVDParser.Parser.Abstract;
using NVDParser.Parser.Entities;
using System;
using System.Collections.Generic;

namespace NVDParser.Parser.Concrete
{
    public class CPEListParser:ICPEListParser
    {
        //use agalitypack for parsing on DOM
        private readonly HtmlWeb web = new HtmlWeb();
        /// <summary>
        /// This method provides to parse all versions on the DOM
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>List<CPE></returns>
        public List<CPE> ParseCPEOnDOM(string uri)
        {
            //define a string list for collected cpe
            List<CPE> CPEs = new List<CPE>();

            //load url
            var htmlDoc = web.Load(uri);

            //get page count
            var pageCountText = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[2]/div[1]/div[2]/strong");

            //convert to int page count
            int pageCount = (Convert.ToInt32(pageCountText.InnerText));

            for (int i = 0; i < pageCount; i = i + 20)
            {
                //load url
                var tables = web.Load(uri + "&startIndex=" + i);

                //parse table
                foreach (HtmlNode table in tables.DocumentNode.SelectNodes("/html/body/div[2]/div[2]/div[2]/table/tbody"))
                {
                    //parse row
                    foreach (HtmlNode row in table.SelectNodes("tr/td/div[2]"))
                    {
                        var vendorName = row.SelectSingleNode("div[1]");
                        var productName = row.SelectSingleNode("div[2]");
                        var version = row.SelectSingleNode("div[3]");
                        var update = row.SelectSingleNode("div[4]");

                        //add list
                        CPEs.Add(new CPE
                        {
                            vendor = vendorName.InnerText.Trim(),
                            product = productName.InnerText.Trim(),
                            version = version.InnerText.Trim(),
                            update = update.InnerText.Trim()

                        });

                    }
                }
            }

            return CPEs;
        }

    }
}
