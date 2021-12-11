using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mode_api.Contracts.Confederates.BattleLanguage.ModeDetail;
using mode_api.Services.Confederates.BattleLanguage;

namespace mode_api.Controllers.Confederates.BattleLanguage 
{
    [ApiController]
    [Route("mode-detail")]
    public class ModeDetailController : ControllerBase 
    {
        private readonly IModeDetailService _modeDetailService; 
        public ModeDetailController(IModeDetailService modeDetailService) 
        { 
            _modeDetailService = modeDetailService;
        }
        
        [HttpGet] public async Task<ActionResult<ModeDetailResponse>> SearchAsync() 
        { 
            var response = await _modeDetailService.SearchByCriteria(); 
            return Ok(response);
        }

        [HttpGet] 
        [Route("/mode-detail/{id}")] 
        public async Task<ActionResult<ModeDetailItem>> GetByExternalIdAsync(Guid id) 
        { 
            var modeDetail = await _modeDetailService.GetByExternalId(id); 
            if ( modeDetail == null ) 
            { 
                return NotFound(); 
            } 
            return Ok(modeDetail); 
        }

        [HttpDelete]
        [Route("/mode-detail/{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(Guid id) 
        {
            var result = await _modeDetailService.Delete(id); 
            return Ok(result); 
        }
        
        [HttpPut] 
        [Route("/mode-detail/{id}")] 
        public async Task<ActionResult<ModeDetailItem>> UpdateAsync(Guid id, ModeDetailUpsert modeDetailToUpdate) 
        { 
            var modeDetail = await _modeDetailService.Update(modeDetailToUpdate, id); 

            if(modeDetail == null) 
            {
                return NotFound();
            }

            return Ok(modeDetail); 
        }
        [HttpPost] 
        public async Task<ActionResult<ModeDetailItem>> CreateAsync(ModeDetailUpsert modeDetailToCreate) 
        { 
            var modeDetail = await _modeDetailService.Create(modeDetailToCreate); 
            return Ok(modeDetail); 
        } 
    } 
}
