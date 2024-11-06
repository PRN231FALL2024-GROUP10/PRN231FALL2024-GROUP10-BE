using JobScial.BAL.DTOs.Profile;
using JobScial.DAL.Infrastructures;
using JobScial.BAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JobScial.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private IAccountRepository _accountRepository;
        private IPostRepository _postRepository;
        private readonly IUnitOfWork unitOfWork;
        public ProfileController(IAccountRepository accountRepository, IUnitOfWork unitOfWork, IPostRepository postRepository)
        {
            _accountRepository = accountRepository;
            this.unitOfWork = unitOfWork;
            _postRepository = postRepository;

        }

        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            var claims = User.Claims.ToList();
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }

            var email = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;


            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email claim is missing.");
            }

            var account = await _accountRepository.GetProfileByEmail(email);

            if (account == null)
            {
                return NotFound("Account not found.");
            }


            return Ok(account);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileByID([FromRoute] int id)
        {
            var claims = User.Claims.ToList();
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }

            var account = await _accountRepository.GetProfileById(id, HttpContext);

            if (account == null)
            {
                return NotFound("Account not found.");
            }


            return Ok(account);
        }

        [HttpGet("{id}/post")]
        public async Task<IActionResult> GetProfilePostByID([FromRoute] int id)
        {
            var claims = User.Claims.ToList();
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }

            var result = await _postRepository.GetPostByAccountIdAsync(id, HttpContext);

            if (result == null)
            {
                return NotFound("Account not found.");
            }


            return Ok(result);
        }

        [HttpGet("{id}/like")]
        public async Task<IActionResult> GetProfileLikeByID([FromRoute] int id)
        {
            var claims = User.Claims.ToList();
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }

            var result = await _postRepository.GetPostByAccountLikeIdAsync(id, HttpContext);

            if (result == null)
            {
                return NotFound("Account not found.");
            }


            return Ok(result);
        }

        [HttpGet("{id}/comment")]
        public async Task<IActionResult> GetProfileCommentByID([FromRoute] int id)
        {
            var claims = User.Claims.ToList();
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }

            var result = await _postRepository.GetPostByAccountCommentIdAsync(id, HttpContext);

            if (result == null)
            {
                return NotFound("Account not found.");
            }


            return Ok(result);
        }

        [HttpGet("{id}/follow")]
        public async Task<IActionResult> GetProfileFollowByID([FromRoute] int id)
        {
            var claims = User.Claims.ToList();
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }

            var result = await _accountRepository.GetByProfileFollow(id, HttpContext);

            if (result == null)
            {
                return NotFound("Account not found.");
            }


            return Ok(result);
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateProfileDto)
        {
            if (updateProfileDto == null)
            {
                return BadRequest("Invalid profile data.");
            }

            var email = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email claim is missing.");
            }

            // Lấy tài khoản hiện tại
            var account = await _accountRepository.UpdateProfile(email,updateProfileDto);

            if (account == null)
            {
                return NotFound("Account not found.");
            }

            return Ok(account);
        }
    }
}
