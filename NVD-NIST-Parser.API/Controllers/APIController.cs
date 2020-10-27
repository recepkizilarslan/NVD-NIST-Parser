using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingPing.NVDParser.Abstract;
using System.Threading.Tasks;

namespace PingPing.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class APIController : ControllerBase
    {
        private readonly INVDParserManager _nvdParserManager;
        private readonly ILogger<APIController> _logger;

        public APIController(ILogger<APIController> logger,INVDParserManager nvdParserManager)
        {
            _nvdParserManager = nvdParserManager;
            _logger = logger;
        }

        /// <summary>
        /// This endpoint proivdes to get all affected CVE of technology
        /// </summary>
        /// <param name="vendorName"></param>
        /// <param name="productName"></param>
        /// <returns>CVE List</returns>
        [HttpGet]
        [Route("GetAllVulnerabilities")]
        public IActionResult GetVulnerabilitiesOfProduct(string vendorName,string productName)
        {
            try 
            {
                var vulnerabilities = _nvdParserManager.ParseAllCVE(vendorName, productName);

                return Ok(vulnerabilities);
            }
            catch(Exception e)
            {
                _logger.LogWarning(e.Message);

                return NotFound();
            }
        }


        /// <summary>
        /// This endpoint proivdes to get all affected CVE of technology
        /// </summary>
        /// <param name="vendorName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetVulnerabilityDetails")]
        public async Task<IActionResult> GetVulnerabilitiesDetail(string cve,string vendorName, string productName)
        {
            try
            {
                var vulnerabilities = await _nvdParserManager.ParseDetails(cve, vendorName, productName);

                return Ok(vulnerabilities);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);

                return NotFound();
            }
        }



    }
}
