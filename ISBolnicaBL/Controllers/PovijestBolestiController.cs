using ISBolnicaDAL.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ISBolnicaBL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PovijestBolestiController : ControllerBase
    {

        private readonly ILogger<PovijestBolestiController> _logger;

        public PovijestBolestiController(ILogger<PovijestBolestiController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllPovijestBolesti")]
        public IEnumerable<string> GetAllPovijestBolesti(int pageNumber, int pageSize)
        {
            return new List<string>();
        }
    }
}
