using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace JobScial.WebAPI.Controllers
{
    public class AccountEduController : Controller
    {
        public readonly IAccountEduRepository _repo;
        public AccountEduController(IAccountEduRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("profile/{accountId}/edu")]
        public IActionResult Get(int accountId)
        {
            return Ok(_repo.GetAll(accountId));
        }

        [HttpPost("profile/{accountId}/edu/create")]
        public async Task<IActionResult> Create([FromBody] AccountEducationDto ent)
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



        [HttpDelete("profile/{accountId}/edu/delete")]
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
