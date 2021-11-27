using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mode_platonic_api.Contracts.Confederates.BattleLanguagePlatonic.ModeDetailPlatonic;
using mode_platonic_api.Services.Confederates.BattleLanguagePlatonic;
namespace mode_platonic_api.Controllers.Confederates.BattleLanguagePlatonic 
{
    [ApiController]
    [Route("mode-detail-platonic")] 
    public class ModeDetailPlatonicController : ControllerBase 
    { 
        private readonly IModeDetailPlatonicService _modeDetailPlatonicService; 
        public ModeDetailPlatonicController(IModeDetailPlatonicService modeDetailPlatonicService) 
        { 
            _modeDetailPlatonicService = modeDetailPlatonicService; }
        
        [HttpGet] public async Task<ActionResult<ModeDetailPlatonicResponse>> SearchAsync() 
        { 
            var response = await _modeDetailPlatonicService.SearchByCriteria(); 
            return Ok(response); 
        }

        [HttpGet] 
        [Route("/mode-detail-platonic/{id}")] 
        public async Task<ActionResult<ModeDetailPlatonicItem>> GetAsync(Guid id) 
        { 
            var modeDetailPlatonic = await _modeDetailPlatonicService.GetById(id); 
            if ( modeDetailPlatonic == null ) 
            { 
                return NotFound(); 
            } 
            return Ok(modeDetailPlatonic); 
        }

        [HttpDelete] 
        public async Task<ActionResult> Delete(IEnumerable<Guid> ids) 
        {
            await _modeDetailPlatonicService.Delete(ids); 
            return Ok(); 
        }
        
        [HttpPut] 
        [Route("/mode-detail-platonic/{id}")] 
        public async Task<ActionResult<ModeDetailPlatonicItem>> Update(Guid id, ModeDetailPlatonicUpsert modeDetailPlatonicToUpdate) 
        { 
            var modeDetailPlatonic = await _modeDetailPlatonicService.Update(modeDetailPlatonicToUpdate, id); 
            return Ok(modeDetailPlatonic); 
        }
        [HttpPost] 
        public async Task<ActionResult<ModeDetailPlatonicItem>> Post(ModeDetailPlatonicUpsert modeDetailPlatonicToCreate) 
        { 
            var modeDetailPlatonic = await _modeDetailPlatonicService.Create(modeDetailPlatonicToCreate); 
            return Ok(modeDetailPlatonic); 
        } 
    } 
}