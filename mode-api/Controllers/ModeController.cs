using System;
using Microsoft.AspNetCore.Mvc;
using mode_api.Contracts.Mode;

namespace mode_api.Controllers
{
    [ApiController]
    [Route("mode")]
    public class ModeController : ControllerBase
    {
        private readonly IModeService _modeService;

        public ModeController(IModeService modeService)
        {
            _modeService = modeService;
        }

        [HttpPost]
        public ActionResult LogMode(LogModeRequest request)
        {
            _modeService.LogMode(request);

            return Ok();
        }
    }
}
