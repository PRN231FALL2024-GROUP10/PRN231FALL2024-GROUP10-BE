using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.DAOs.Interfaces;
using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace JobScial.WebAPI.Controllers
{
    public class AccountCertController : Controller
    {
        public readonly IAccountCertRepository _repo;
        public AccountCertController(IAccountCertRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("profile/{accountId}/cert")]
        public IActionResult Get(int accountId)
        {
            return Ok(_repo.GetAll(accountId));
        }

        [HttpPost("profile/{accountId}/cert/create")]
        public async Task<IActionResult> Create([FromBody] AccountCertificateDto ent)
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

        

        [HttpDelete("profile/{accountId}/cert/delete")]
        public async Task<IActionResult> Delete([FromBody] SubAccountResponse ent)
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
