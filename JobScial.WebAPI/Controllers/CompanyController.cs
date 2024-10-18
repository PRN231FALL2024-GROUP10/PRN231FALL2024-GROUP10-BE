using JobScial.DAL.DAOs.Interfaces;
using JobScial.BAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace JobScial.WebAPI.Controllers
{
    public class CompanyController : Controller
    {
        public readonly ISharedRepository _repo;
        public CompanyController(ISharedRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("company")]
        public IActionResult Get()
        {
            return Ok(_repo.GetAllCompany());
        }
    }
}
