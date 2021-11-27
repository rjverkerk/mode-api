using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using mode_api.Contracts.Confederates.BattleLanguage.ModeDetail;

namespace mode_api.Controllers.Confederates.BattleLanguage
{
    [ApiController]
    [Route("mode-detail")]
    public class ModeDetailController : ControllerBase
    {

        [HttpGet]
        public ActionResult<ModeDetailResponse> Search([FromQuery]ModeDetailSearchCriteria searchCriteria)
        {
            return Ok(new ModeDetailResponse());
        }

        [HttpGet]
        [Route("/mode-detail/{id}")]
        public ActionResult<ModeDetail> Get(string id)
        {
            return Ok(new ModeDetail());
        }

        [HttpDelete]
        public ActionResult<IEnumerable<string>> Delete(IEnumerable<string> ids)
        {
            return Ok(ids);
        }

        [HttpPut]
        public ActionResult<ModeDetail> Update(ModeDetail modeToUpdate)
        {
            return Ok(modeToUpdate);
        }

        [HttpPost]
        public ActionResult<ModeDetail> Post(ModeDetail modeToCreate)
        {
            return Ok(modeToCreate);
        }
    }
}
