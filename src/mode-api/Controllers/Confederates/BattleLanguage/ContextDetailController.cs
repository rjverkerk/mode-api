using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mode_api.Contracts.Confederates.BattleLanguage.ContextDetail;
using mode_api.Services.Confederates.BattleLanguage;
namespace mode_api.Controllers.Confederates.BattleLanguage 
{
    [ApiController]
    [Route("context-detail")] 
    public class ContextDetailController : ControllerBase 
    { 
        private readonly IContextDetailService _contextDetailService; 
        public ContextDetailController(IContextDetailService contextDetailService) 
        { 
            _contextDetailService = contextDetailService; }
        
        [HttpGet] public async Task<ActionResult<ContextDetailResponse>> SearchAsync() 
        { 
            var response = await _contextDetailService.SearchByCriteria(); 
            return Ok(response); 
        }

        [HttpGet] 
        [Route("/context-detail/{id}")] 
        public async Task<ActionResult<ContextDetailItem>> GetAsync(Guid id) 
        { 
            var contextDetail = await _contextDetailService.GetById(id); 
            if ( contextDetail == null ) 
            { 
                return NotFound(); 
            } 
            return Ok(contextDetail); 
        }

        [HttpDelete] 
        public async Task<ActionResult> Delete(IEnumerable<Guid> ids) 
        {
            await _contextDetailService.Delete(ids); 
            return Ok(); 
        }
        
        [HttpPut] 
        [Route("/context-detail/{id}")] 
        public async Task<ActionResult<ContextDetailItem>> Update(Guid id, ContextDetailUpsert contextDetailToUpdate) 
        { 
            var contextDetail = await _contextDetailService.Update(contextDetailToUpdate, id); 
            return Ok(contextDetail); 
        }
        [HttpPost] 
        public async Task<ActionResult<ContextDetailItem>> Post(ContextDetailUpsert contextDetailToCreate) 
        { 
            var contextDetail = await _contextDetailService.Create(contextDetailToCreate); 
            return Ok(contextDetail); 
        } 
    } 
}
