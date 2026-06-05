using FlashcardAPI.Model;
using FlashcardAPI.Service;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace FlashcardAPI.Controllers
{
    [ApiController]                    
    [Route("api/folders")]
    public class FolderController : ControllerBase
    {
        private readonly FolderService _folderService;
        
        public FolderController(FolderService folderService)
        {
            _folderService = folderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _folderService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Folder folderInput)
        {
            if (folderInput == null || string.IsNullOrEmpty(folderInput.Title))
            {
                return BadRequest("Tên thư mục không được để trống."); 
            }
            await _folderService.CreatceAsync(folderInput);
            return Ok();
        }

    }
}
