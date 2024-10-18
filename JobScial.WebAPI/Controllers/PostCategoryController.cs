using JobScial.BAL.DTOs.PostCategory;
using JobScial.DAL.DAOs.Implements;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using JobScial.BAL.Repositorys.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace JobScial.WebAPI.Controllers
{
    public class PostCategoryController : ODataController
    {
        private IPostCategoryRepository _postCategoryRepository;

        public PostCategoryController(IPostCategoryRepository postCategoryRepository)
        {
            _postCategoryRepository = postCategoryRepository;
        }

        [HttpGet("PostCategory")]
        public async Task<IActionResult> GetAllPostCategories()
        {
            var postCategories = await _postCategoryRepository.getAll();
            return Ok(postCategories);
        }

        [HttpGet("odata/PostCategory/{id}")]
        [EnableQuery]
        public async Task<IActionResult> GetPostCategoryById(int id)
        {
            var postCategory = await _postCategoryRepository.getById(id);
            if (postCategory == null)
            {
                return NotFound();
            }
            return Ok(postCategory);
        }

        
        [HttpPost("PostCategory")]
        public async Task<IActionResult> CreatePostCategory([FromBody] PostCategoryDto postCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postCategory = new PostCategory
            {
                Name = postCategoryDto.Name
            };
            await _postCategoryRepository.add(postCategory);
            return Created(postCategory);
        }

        [HttpPut("PostCategory/{id}")]
        public async Task<IActionResult> UpdatePostCategory(int id, [FromBody] PostCategoryDto updatedPostCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postCategory = await _postCategoryRepository.getById(id);
            if (postCategory == null)
            {
                return NotFound();
            }
            postCategory.Name = updatedPostCategory.Name;
            await _postCategoryRepository.update(id, postCategory);
            return NoContent();
        }

        [HttpDelete("PostCategory/{id}")]
        public async Task<IActionResult> DeletePostCategory(int id)
        {
            var postCategory = await _postCategoryRepository.getById(id);
            if (postCategory == null)
            {
                return NotFound();
            }

            await _postCategoryRepository.delete(id);
            return NoContent();
        }
    }
}

