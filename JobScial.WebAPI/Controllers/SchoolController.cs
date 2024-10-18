using JobScial.BAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace JobScial.WebAPI.Controllers
{
    public class SchoolController : Controller
    {
        public readonly ISharedRepository _repo;
        public SchoolController(ISharedRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("school")]
        public IActionResult Get()
        {
            return Ok(_repo.GetAllSchool());
        }
    }
}
