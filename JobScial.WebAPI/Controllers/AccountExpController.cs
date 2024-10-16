using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace JobScial.WebAPI.Controllers
{
    public class AccountExpController : Controller
    {
        public readonly IAccountExpRepository _repo;
        public AccountExpController(IAccountExpRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("profile/{accountId}/exp")]
        public IActionResult Get(int accountId)
        {
            return Ok(_repo.GetAll(accountId));
        }

        [HttpPost("profile/{accountId}/exp/create")]
        public async Task<IActionResult> Create([FromBody] AccountExperienceDto ent)
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



        [HttpDelete("profile/{accountId}/exp/delete")]
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
