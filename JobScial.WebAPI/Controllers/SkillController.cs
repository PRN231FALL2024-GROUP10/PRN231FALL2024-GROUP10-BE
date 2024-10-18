using JobScial.BAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace JobScial.WebAPI.Controllers
{
    public class SkillController : Controller
    {
        public readonly ISharedRepository _repo;
        public SkillController(ISharedRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("skill")]
        public IActionResult Get()
        {
            return Ok(_repo.GetAllSkillCategory());
        }
    }
}
