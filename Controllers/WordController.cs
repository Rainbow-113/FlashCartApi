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

        [HttpGet("{folderId}")]
        public async Task<IActionResult> GetByFolder(string folderId)
        {
            if (string.IsNullOrEmpty(folderId)) return BadRequest("FolderId không hợp lệ.");
            var result = await _wordService.GetWordsByFolderIdAsync(folderId);
            return Ok(result);
        }
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Id không hợp lệ.");
            var word = await _wordService.GetByIdAsync(id);
            if (word == null) return NotFound("Không tìm thấy từ vựng.");
            return Ok(word);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Word wordInput)
        {
            if (wordInput == null || string.IsNullOrEmpty(wordInput.English))
                return BadRequest("Từ tiếng Anh không được để trống.");
            await _wordService.CreateAsync(wordInput);
            return Ok("Thêm thành công");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var word = await _wordService.GetByIdAsync(id);
            if (word == null) return NotFound("Không tìm thấy từ vựng.");
            await _wordService.DeleteAsync(id);
            return Ok("Xóa thành công");
        }
        //
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Word wordInput)
        {
            if (wordInput == null || string.IsNullOrEmpty(wordInput.English))
                return BadRequest("Từ tiếng Anh không được để trống.");

            var word = await _wordService.GetByIdAsync(id);
            if (word == null) return NotFound("Không tìm thấy từ vựng.");

            try
            {
                // ✅ Chỉ cho sửa các field này, giữ nguyên Id, FolderId, CreatedAt
                word.English = wordInput.English;
                word.ExampleEnglish = wordInput.ExampleEnglish;
                word.Vietnamese = wordInput.Vietnamese;
                word.ExampleVietnamese = wordInput.ExampleVietnamese;
                word.Interval = wordInput.Interval;
                word.IsDueToday = wordInput.IsDueToday;

                await _wordService.UpdateAsync(id, word);
                return Ok("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

