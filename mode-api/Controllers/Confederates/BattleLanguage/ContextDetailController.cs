using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using mode_api.Contracts.Confederates.BattleLanguage.ContextDetail;

namespace mode_api.Controllers.Confederates.BattleLanguage
{
    [ApiController]
    [Route("context-detail")]
    public class ContextDetailController : ControllerBase
    {
        [HttpGet]
        public ActionResult<ContextDetailResponse> Search([FromQuery]ContextSearchCriteria searchCriteria)
        {
            return Ok(new ContextDetailResponse());
        }

        [HttpGet]
        [Route("/context-detail/{id}")]
        public ActionResult<ContextDetail> Get(string id)
        {
            return Ok(new ContextDetail());
        }

        [HttpDelete]
        public ActionResult<IEnumerable<string>> Delete(IEnumerable<string> ids)
        {
            return Ok(ids);
        }

        [HttpPut]
        public ActionResult<ContextDetail> Update(ContextDetail contextToUpdate)
        {
            return Ok(contextToUpdate);
        }

        [HttpPost]
        public ActionResult<ContextDetail> Post(ContextDetail contextToCreate)
        {
            return Ok(contextToCreate);
        }
    }
}
