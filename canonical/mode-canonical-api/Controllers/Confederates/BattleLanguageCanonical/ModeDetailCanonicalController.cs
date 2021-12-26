using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mode_canonical_api.Contracts.Confederates.BattleLanguageCanonical.ModeDetailCanonical;
using mode_canonical_api.Services.Confederates.BattleLanguageCanonical;

namespace mode_canonical_api.Controllers.Confederates.BattleLanguageCanonical 
{
    [ApiController]
    [Route("mode-detail-canonicals")]
    public class ModeDetailCanonicalController : ControllerBase 
    {
        private readonly IModeDetailCanonicalService _modeDetailCanonicalService; 
        public ModeDetailCanonicalController(IModeDetailCanonicalService modeDetailCanonicalService) 
        { 
            _modeDetailCanonicalService = modeDetailCanonicalService;
        }
        
        [HttpGet("/search", Name = nameof(SearchAsync))]
        public async Task<ActionResult<ModeDetailCanonicalResponse>> SearchAsync() 
        { 
            var response = await _modeDetailCanonicalService.SearchByCriteria(); 
            return Ok(response);
        }

        [HttpGet("{id}", Name = nameof(GetByExternalIdAsync))]
        public async Task<ActionResult<ModeDetailCanonicalItem>> GetByExternalIdAsync(Guid id) 
        { 
            var modeDetailCanonical = await _modeDetailCanonicalService.GetByExternalId(id); 
            if ( modeDetailCanonical == null ) 
            { 
                return NotFound(); 
            } 
            return Ok(modeDetailCanonical); 
        }

        [HttpDelete("{id}", Name = nameof(DeleteAsync))]
        public async Task<ActionResult<bool>> DeleteAsync(Guid id) 
        {
            var result = await _modeDetailCanonicalService.Delete(id); 
            return Ok(result); 
        }
        
        [HttpPut("{id}", Name = nameof(UpdateAsync))]
        public async Task<ActionResult<ModeDetailCanonicalItem>> UpdateAsync(Guid id, ModeDetailCanonicalUpsert modeDetailCanonicalToUpdate) 
        { 
            var modeDetailCanonical = await _modeDetailCanonicalService.Update(modeDetailCanonicalToUpdate, id); 

            if(modeDetailCanonical == null) 
            {
                return NotFound();
            }

            return Ok(modeDetailCanonical);
        }

        [HttpPost(Name = nameof(CreateAsync))] 
        public async Task<ActionResult<ModeDetailCanonicalItem>> CreateAsync(ModeDetailCanonicalUpsert modeDetailCanonicalToCreate) 
        { 
            var modeDetailCanonical = await _modeDetailCanonicalService.Create(modeDetailCanonicalToCreate); 
            return Ok(modeDetailCanonical); 
        } 
    } 
}
