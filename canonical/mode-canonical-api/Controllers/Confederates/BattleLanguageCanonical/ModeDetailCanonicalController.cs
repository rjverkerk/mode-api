using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mode_canonical_api.Contracts.Confederates.BattleLanguageCanonical.ModeDetailCanonical;
using mode_canonical_api.Services.Confederates.BattleLanguageCanonical;

namespace mode_canonical_api.Controllers.Confederates.BattleLanguageCanonical 
{
    [ApiController]
    [Route("mode-detail-canonical")]
    public class ModeDetailCanonicalController : ControllerBase 
    {
        private readonly IModeDetailCanonicalService _modeDetailCanonicalService; 
        public ModeDetailCanonicalController(IModeDetailCanonicalService modeDetailCanonicalService) 
        { 
            _modeDetailCanonicalService = modeDetailCanonicalService;
        }
        
        [HttpGet] public async Task<ActionResult<ModeDetailCanonicalResponse>> SearchAsync() 
        { 
            var response = await _modeDetailCanonicalService.SearchByCriteria(); 
            return Ok(response);
        }

        [HttpGet] 
        [Route("/mode-detail-canonical/{id}")] 
        public async Task<ActionResult<ModeDetailCanonicalItem>> GetByExternalIdAsync(Guid id) 
        { 
            var modeDetailCanonical = await _modeDetailCanonicalService.GetByExternalId(id); 
            if ( modeDetailCanonical == null ) 
            { 
                return NotFound(); 
            } 
            return Ok(modeDetailCanonical); 
        }

        [HttpDelete]
        [Route("/mode-detail-canonical/{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(Guid id) 
        {
            var result = await _modeDetailCanonicalService.Delete(id); 
            return Ok(result); 
        }
        
        [HttpPut] 
        [Route("/mode-detail-canonical/{id}")] 
        public async Task<ActionResult<ModeDetailCanonicalItem>> UpdateAsync(Guid id, ModeDetailCanonicalUpsert modeDetailCanonicalToUpdate) 
        { 
            var modeDetailCanonical = await _modeDetailCanonicalService.Update(modeDetailCanonicalToUpdate, id); 

            if(modeDetailCanonical == null) 
            {
                return NotFound();
            }

            return Ok(modeDetailCanonical); 
        }
        [HttpPost] 
        public async Task<ActionResult<ModeDetailCanonicalItem>> CreateAsync(ModeDetailCanonicalUpsert modeDetailCanonicalToCreate) 
        { 
            var modeDetailCanonical = await _modeDetailCanonicalService.Create(modeDetailCanonicalToCreate); 
            return Ok(modeDetailCanonical); 
        } 
    } 
}
