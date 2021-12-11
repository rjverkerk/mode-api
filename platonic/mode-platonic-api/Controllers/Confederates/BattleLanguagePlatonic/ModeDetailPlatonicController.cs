using System;
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
            _modeDetailPlatonicService = modeDetailPlatonicService;
        }
        
        [HttpGet] public async Task<ActionResult<ModeDetailPlatonicResponse>> SearchAsync() 
        { 
            var response = await _modeDetailPlatonicService.SearchByCriteria(); 
            return Ok(response);
        }

        [HttpGet] 
        [Route("/mode-detail-platonic/{id}")] 
        public async Task<ActionResult<ModeDetailPlatonicItem>> GetByExternalIdAsync(Guid id) 
        { 
            var modeDetailPlatonic = await _modeDetailPlatonicService.GetByExternalId(id); 
            if ( modeDetailPlatonic == null ) 
            { 
                return NotFound(); 
            } 
            return Ok(modeDetailPlatonic); 
        }

        [HttpDelete]
        [Route("/mode-detail-platonic/{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(Guid id) 
        {
            var result = await _modeDetailPlatonicService.Delete(id); 
            return Ok(result); 
        }
        
        [HttpPut] 
        [Route("/mode-detail-platonic/{id}")] 
        public async Task<ActionResult<ModeDetailPlatonicItem>> UpdateAsync(Guid id, ModeDetailPlatonicUpsert modeDetailPlatonicToUpdate) 
        { 
            var modeDetailPlatonic = await _modeDetailPlatonicService.Update(modeDetailPlatonicToUpdate, id); 

            if(modeDetailPlatonic == null) 
            {
                return NotFound();
            }

            return Ok(modeDetailPlatonic); 
        }
        [HttpPost] 
        public async Task<ActionResult<ModeDetailPlatonicItem>> CreateAsync(ModeDetailPlatonicUpsert modeDetailPlatonicToCreate) 
        { 
            var modeDetailPlatonic = await _modeDetailPlatonicService.Create(modeDetailPlatonicToCreate); 
            return Ok(modeDetailPlatonic); 
        } 
    } 
}