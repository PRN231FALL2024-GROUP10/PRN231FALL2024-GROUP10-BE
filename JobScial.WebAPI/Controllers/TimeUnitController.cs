using JobScial.BAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace JobScial.WebAPI.Controllers
{
    public class TimeUnitController : Controller
    {
        public readonly ISharedRepository _repo;
        public TimeUnitController(ISharedRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("TimeUnit")]
        public IActionResult Get()
        {
            return Ok(_repo.GetAllTimespanUnit());
        }
    }
}
