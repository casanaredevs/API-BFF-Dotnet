using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Bff.Api.Controllers
{
    [Route("api/BFF")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        readonly IOptions<List<UrlModels>> _urls;
        public RedirectController(
            IOptions<List<UrlModels>> urls)
        {
            _urls = urls;
        }

        [HttpPost("{apiName}/{url}")]
        [HttpGet("{apiName}/{url}")]
        [HttpPut("{apiName}/{url}")]
        [HttpDelete("{apiName}/{url}")]
        public IActionResult Redirect(string apiName, string url)
        {
            var registeredApi = _urls.Value?.FirstOrDefault(x => x.ApiName == apiName) ?? null;

            if (registeredApi == null)
                return NotFound();

            var completrUrl = registeredApi.Url + url;


            return RedirectPermanentPreserveMethod(completrUrl);
        }
    }
}
