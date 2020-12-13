using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using NVDParser.Parser.Abstract;
using NVDParser.Parser.Entities;

namespace NVDParser.Parser.Concrete
{
    public class AffectedCPEParser : IAffectedCPEParser
    {
        private readonly HttpClient client = new HttpClient();

        /// <summary>
        /// This method is provides parse containers and finds cpe id's
        /// </summary>
        /// <param name="json"></param>
        /// <returns>List<string></returns>
        public List<string> ParseCPEMap(string json)
        {
            //create a url list
            List<string> CpeURLs = new List<string>();

            //decode html 
            var DecodedJson = System.Web.HttpUtility.HtmlDecode(json);

            //parse json
            List<AffecteCPE> cpes = JsonSerializer.Deserialize<List<AffecteCPE>>(DecodedJson);

            //read json data
            for (int j = 0; j <= cpes.Count - 1; j++)
            {
                for (int k = 0; k <= cpes[j].containers[0].cpes.Count - 1; k++)
                {
                    //generate cpe url and add list
                    CpeURLs.Add("https://nvd.nist.gov/rest/public/cpe/match?cpeFactId=" + cpes[j].containers[0].cpes[k].id + "&resultsPerPage=99999");
                }
            }
            //return list
            return CpeURLs;
        }

        /// <summary>
        /// this method provides to get cpe by http client
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>CPE Result</returns>
        public async Task<CPEResult> GetCPE(string uri)
        {
            //Clear default request header
            client.DefaultRequestHeaders.Accept.Clear();

            //Add "application/json" header
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //parse JSON
            return await JsonSerializer.DeserializeAsync<CPEResult>(await client.GetStreamAsync(uri));
        }

        /// <summary>
        /// This method provides to parse cpe list and add values to List
        /// </summary>
        /// <param name="cpeResult"></param>
        /// <param name="vendorName"></param>
        /// <param name="productName"></param>
        /// <returns>Affected version</returns>
        public List<string> ParseCPE(CPEResult cpeResult,string vendorName,string productName)
        {
            List<string> cpes = new List<string>();

            //read json result
            for (int m = 0; m < cpeResult.cpes.cpes.Count; m++)
            {
                if (cpeResult.cpes.cpes[m].vendor != vendorName || cpeResult.cpes.cpes[m].product != productName)
                {
                    continue;
                }

                cpes.Add(cpeResult.cpes.cpes[m].version);
            }
            //return cpe
            return cpes;
        }
    }
}

