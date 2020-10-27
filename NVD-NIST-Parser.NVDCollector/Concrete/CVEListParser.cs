using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using PingPing.NVDParser.Abstract;

namespace PingPing.NVDParser.Concrete
{
    public class CVEListParser : ICVEListParser
    {
        private readonly HtmlWeb web = new HtmlWeb();

        /// <summary>
        /// This method provides parse all CVE's of products
        /// </summary>
        /// <param name="url"></param>
        /// <returns>All CVE List</returns>
        public List<string> ParsePage(string url)
        {
            //define a string queue for collected cve
            List<string> crawledCVEs = new List<string>();

            //load url
            var htmlDoc = web.Load(url);

            //get page count
            var pageCountText = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[2]/div/div[1]/div[2]/strong");

            //convert to int page count
            int pageCount = (Convert.ToInt32(pageCountText.InnerText));

            for (int i = 0; i < pageCount; i = i + 20)
            {
                //load url
                var tables = web.Load($"{url}&startIndex={i}");

                //parse table
                foreach (HtmlNode table in tables.DocumentNode.SelectNodes("//*[@id=\"row\"]/table/tbody/tr"))
                {
                    //parse row
                    foreach (HtmlNode row in table.SelectNodes("th/strong/a"))
                    {
                        //add queue
                        crawledCVEs.Add(row.InnerText);
                    }
                }
            }

            return crawledCVEs;
        }
    }
}
