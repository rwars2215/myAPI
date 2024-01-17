using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<string> Get()
        {
            return Summaries;
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            Summaries.Add(name);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Индекс не верный!");
            }

            Summaries[index] = name;
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Индекс не верный!");
            }

            Summaries.RemoveAt(index);
            return Ok();
        }

        [HttpGet("{index}")]
        public string GetName(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return "Индекс не верный!";
            }
            return Summaries[index];
        }

        [HttpGet("find-by-name")]
        public int GetQuantity(string name)
        {
            int acc = 0;
            for (int i = 0; i < Summaries.Count; i++)
            {
                if (Summaries[i] == name)
                {
                    acc++;
                }
            }
            return acc;
        }

        [HttpGet("sort-array")]
        public IActionResult GetAll(int? sortStrategy)
        {
            switch (sortStrategy)
            {
                case null:
                    return Ok(Summaries);
                case 1:
                    Summaries.Sort();
                    return Ok(Summaries);
                case -1:
                    Summaries.Reverse();
                    return Ok(Summaries);
                default:
                    return BadRequest("Некорректное значение параметра sortStrategy");
            }
        }

    }
}