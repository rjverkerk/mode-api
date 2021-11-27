using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mode_platonic_api.Contracts.Confederates.BattleLanguagePlatonic.ContextDetailPlatonic;
using mode_platonic_api.Services.Confederates.BattleLanguagePlatonic;
namespace mode_platonic_api.Controllers.Confederates.BattleLanguagePlatonic 
{
    [ApiController]
    [Route("context-detail-platonic")] 
    public class ContextDetailPlatonicController : ControllerBase 
    { 
        private readonly IContextDetailPlatonicService _contextDetailPlatonicService; 
        public ContextDetailPlatonicController(IContextDetailPlatonicService contextDetailPlatonicService) 
        { 
            _contextDetailPlatonicService = contextDetailPlatonicService; }
        
        [HttpGet] public async Task<ActionResult<ContextDetailPlatonicResponse>> SearchAsync() 
        { 
            var response = await _contextDetailPlatonicService.SearchByCriteria(); 
            return Ok(response); 
        }

        [HttpGet] 
        [Route("/context-detail-platonic/{id}")] 
        public async Task<ActionResult<ContextDetailPlatonicItem>> GetAsync(Guid id) 
        { 
            var contextDetailPlatonic = await _contextDetailPlatonicService.GetById(id); 
            if ( contextDetailPlatonic == null ) 
            { 
                return NotFound(); 
            } 
            return Ok(contextDetailPlatonic); 
        }

        [HttpDelete] 
        public async Task<ActionResult> Delete(IEnumerable<Guid> ids) 
        {
            await _contextDetailPlatonicService.Delete(ids); 
            return Ok(); 
        }
        
        [HttpPut] 
        [Route("/context-detail-platonic/{id}")] 
        public async Task<ActionResult<ContextDetailPlatonicItem>> Update(Guid id, ContextDetailPlatonicUpsert contextDetailPlatonicToUpdate) 
        { 
            var contextDetailPlatonic = await _contextDetailPlatonicService.Update(contextDetailPlatonicToUpdate, id); 
            return Ok(contextDetailPlatonic); 
        }
        [HttpPost] 
        public async Task<ActionResult<ContextDetailPlatonicItem>> Post(ContextDetailPlatonicUpsert contextDetailPlatonicToCreate) 
        { 
            var contextDetailPlatonic = await _contextDetailPlatonicService.Create(contextDetailPlatonicToCreate); 
            return Ok(contextDetailPlatonic); 
        } 
    } 
}
