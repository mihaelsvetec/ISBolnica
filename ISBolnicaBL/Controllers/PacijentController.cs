using ISBolnicaBL.Pacijent;
using ISBolnicaDAL.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ISBolnicaBL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PacijentController : ControllerBase
    {

        private readonly ILogger<PacijentController> _logger;
        private readonly IPacijentService _pacijentService;

        public PacijentController(ILogger<PacijentController> logger)
        {
            _logger = logger;
            _pacijentService = new PacijentService();
        }

        [HttpGet(Name = "GetAllPacijent")]
        public IActionResult GetAllPacijent(int pageNumber, int pageSize)
        {
            return Ok(_pacijentService.GetAllPacijent(pageNumber, pageSize));
        }
    }
}
