using Microsoft.AspNetCore.Mvc;

namespace server.Controllers 
{
    [ApiController]
    [Route("api/level")]
    public class LevelController : ControllerBase
    {
        private readonly LevelService _levelService;

        public LevelController(LevelService levelService)
        {
            _levelService = levelService;
        }

        // GET /api/level?page=1&pageSize=10
        [HttpGet]
        public ActionResult<PagedResult<LevelSummary>> Get(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var (levels, totalItems) = _levelService.Get(page, pageSize, search);
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var summaries = levels
                .Select(l => new LevelSummary
                {
                    Id = l.Id,
                    Name = l.Name,
                    Author = l.Author
                })
                .ToList();

            return new PagedResult<LevelSummary>
            {
                Items = summaries,
                Page = page,
                PageSize = pageSize,
                TotalItems = (int)totalItems,
                TotalPages = totalPages
            };
        }


        // GET /api/level/{id}
        [HttpGet("{id}")]
        public ActionResult<Level> Get(string id)
        {
            var level = _levelService.Get(id);
            if (level == null) return NotFound();
            return level;
        }

        // POST /api/level
        [HttpPost]
        public ActionResult<Level> Create([FromBody] JsonLevel jsonLevel)
        {
            var level = new Level
            {
                Name = jsonLevel.Name,
                Author = jsonLevel.Author,
                Content = jsonLevel.Content,
                UploadDate = DateTime.UtcNow
            };

            _levelService.Create(level);
            return CreatedAtAction(nameof(Get), new { id = level.Id }, level);
        }

        // DELETE /api/level/{id}
        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {
            var existing = _levelService.Get(id);
            if (existing == null) return NotFound();

            _levelService.Remove(id);
            return NoContent();
        }
    }
}
