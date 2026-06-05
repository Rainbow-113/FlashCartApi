using FlashcardAPI.Model;
using FlashcardAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardAPI.Controllers
{
    [ApiController]
    [Route("api/words")]
    public class WordController : ControllerBase
    {
        private readonly WordService _wordService;
        public WordController(WordService wordService)
        {
            _wordService = wordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByFolder(string folderId)
        {
            if (string.IsNullOrEmpty(folderId)) return BadRequest("FolderId không hợp lệ.");
            var result = await _wordService.GetWordsByFolderIdAsync(folderId);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Word wordInput)
        {
            if (wordInput == null || string.IsNullOrEmpty(wordInput.FolderId))
            {
                return BadRequest("Thiếu thông tin thư mục liên kết (FolderId).");
            }
            ;
            if (string.IsNullOrEmpty(wordInput.English))
            {
                return BadRequest("Từ (EN) và Nghĩa (VI) không được để trống.");
            }
            ;
            try
            {
                await _wordService.CreateWordAsync(wordInput);
                return Ok(wordInput);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Lỗi lưu database: {ex.Message}");
            }
        }
    }
}

