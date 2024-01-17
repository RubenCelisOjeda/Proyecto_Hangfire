using Hangfire;
using Microsoft.AspNetCore.Mvc;
using prueba.WebApi.Dto;

namespace prueba.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly ILogger<ArticuloController> _logger;

        public ArticuloController(ILogger<ArticuloController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "GetArticulo")]
        public IEnumerable<WeatherForecast> GetArticulo([FromBody] ArticuloRequestDto itemRequest)
        {

            Task.Run(() => {

                var fechaPublicacion = itemRequest.FechaPublicacion;
                var client = new BackgroundJobClient();
                client.Schedule(() => Console.WriteLine("test.........!"), TimeSpan.FromSeconds(10));
            });

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
            })
            .ToArray();
        }

        private void Imprimir(object callback)
        {
            _logger.LogInformation("test");

            Console.WriteLine("Proceso en ....");
        }
    }
}
