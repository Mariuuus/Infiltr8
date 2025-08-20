using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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

        [HttpGet]
        public ActionResult<List<Level>> Get() => _levelService.Get();

        [HttpGet("{id}")]
        public ActionResult<Level> Get(string id) => _levelService.Get(id);

        [HttpPost]
        public ActionResult<Level> Create([FromBody] object levelJson)
        {
            var json = levelJson.ToString();
            var level = new Level { Content = json };
            _levelService.Create(level);
            return level;
        }

        [HttpDelete("{id}")]
        public void Remove(string id) => _levelService.Remove(id);

    }
}