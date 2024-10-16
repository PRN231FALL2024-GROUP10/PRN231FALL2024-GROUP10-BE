using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace JobScial.WebAPI.Controllers
{
    public class JobTitleController : Controller
    {
        public readonly ISharedRepository _repo;
        public JobTitleController(ISharedRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("JobTitle")]
        public IActionResult Get()
        {
            return Ok(_repo.GetAllJobTitle());
        }
    }
}
