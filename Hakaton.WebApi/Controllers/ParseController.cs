using Hakaton.Application.Parser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Hakaton.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParseController: ControllerBase
    {
        private readonly ParseManager _parseManager;
        public ParseController (ParseManager parseManager)
            => _parseManager = parseManager;


        [HttpGet]
        [Authorize]
        public async Task<string> GetCode(string pnr)
        {
            var code = pnr;
            if (string.IsNullOrEmpty(code))
            {
                throw new Exception("Empty line!");
            }
            var humanReadableString = _parseManager.Parse(code);
            return humanReadableString;
        }


    }
}
