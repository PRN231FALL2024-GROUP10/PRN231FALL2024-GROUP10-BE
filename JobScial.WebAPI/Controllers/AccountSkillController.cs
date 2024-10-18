using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace JobScial.WebAPI.Controllers
{
    public class AccountSkillController : Controller
    {
        public readonly IAccountSkillRepository _repo;
        public AccountSkillController(IAccountSkillRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("profile/{accountId}/skill")]
        public IActionResult Get(int accountId)
        {
            return Ok(_repo.GetAll(accountId));
        }

        [HttpPost("profile/{accountId}/skill/create")]
        public async Task<IActionResult> Create([FromBody] AccountSkillDto ent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(x => x.Value?.Errors)
                           .Where(y => y.Count > 0)
                           .ToList());
            }

            if (await _repo.Save(ent))
            {
                return Ok("Success");
            }

            return BadRequest("Error");
        }



        [HttpDelete("profile/{accountId}/skill/delete")]
        public async Task<IActionResult> Delete(SubAccountResponse ent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(x => x.Value?.Errors)
                           .Where(y => y.Count > 0)
                           .ToList());
            }

            if (await _repo.Delete(ent.AccountId, ent.Id))
            {
                return Ok("Success");
            }

            return BadRequest("Error");
        }
    }
}
