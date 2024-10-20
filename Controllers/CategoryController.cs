using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using No_Overspend_Api.Base;
using No_Overspend_Api.DTOs.Base;
using No_Overspend_Api.DTOs.Category.Request;
using No_Overspend_Api.DTOs.Category.Response;
using No_Overspend_Api.Infra.Routes;
using No_Overspend_Api.Services;

namespace No_Overspend_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : AuthorizeController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet(CategoryRoutes.Search)]
        public async Task<IActionResult> Search(CategoryFilter request)
        {
            return Ok(await _categoryService.SearchAsync(UserHeader.user_id, request));
        }
        [HttpGet(CategoryRoutes.GetDetail)]
        public async Task<IActionResult> GetDetail([FromRoute] GetDetailRequest request)
        {
            return Ok(await _categoryService.GetDetailAsync(UserHeader.user_id, request));
        }
        [HttpPost(CategoryRoutes.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            return Ok(await _categoryService.CreateAsync(UserHeader.user_id, request));
        }
        [HttpPut(CategoryRoutes.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest request)
        {
            return Ok(await _categoryService.UpdateAsync(UserHeader.user_id, request));
        }
        [HttpDelete(CategoryRoutes.Delete)]
        public async Task<IActionResult> Delete([FromBody] DeleteRequest request)
        {
            return Ok(await _categoryService.DeleteAsync(UserHeader.user_id, request));
        }
    }
}
