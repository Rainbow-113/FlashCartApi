using FlashcardAPI.Model;
using FlashcardAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Security.Claims;

namespace FlashcardAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/folders")]
    public class FolderController : ControllerBase
    {
        private readonly FolderService _folderService;
        
        public FolderController(FolderService folderService)
        {
            _folderService = folderService;
        }
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
        [HttpGet]
        public async Task<List<Folder>> GetAll()
        {
            return  await _folderService.GetAllAsync(GetUserId());
           
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var folder = await _folderService.GetByIdAsync(id);
            if (folder == null) return NotFound("Không tìm thấy folder.");
            return Ok(folder);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Folder folderInput)
        {
            if (folderInput == null || string.IsNullOrEmpty(folderInput.Title))
                return BadRequest("Tên thư mục không được để trống.");
            try
            {
                await _folderService.CreatceAsync(folderInput, GetUserId());
                return Ok("Thêm thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var folder = await _folderService.GetByIdAsync(id);
            if (folder == null) return NotFound("Không tìm thấy folder.");
            await _folderService.DeleteAsync(id);
            return Ok("Xóa thành công");
        }
        //update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Folder folderInput)
        {
            if (folderInput == null || string.IsNullOrEmpty(folderInput.Title))
                return BadRequest("Tên thư mục không được để trống.");
            var folder = await _folderService.GetByIdAsync(id);
            if (folder == null) return NotFound("Không tìm thấy folder.");
            try
            {
                folderInput.Id = id;
                await _folderService.UpdateAsync(id, folderInput);
                return Ok("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var userId = User.FindFirst("userId")?.Value
                          ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _folderService.SearchAsync(userId, keyword);
            return Ok(result);
        }
    }
}
