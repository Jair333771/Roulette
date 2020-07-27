using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using RouletteApp.Business.Logic;
using RouletteApp.Data.Emtities;

namespace RouletteApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [DataContract]
    public class RouletteController : ControllerBase
    {
        protected readonly RouletteBll rouletteBll;
        protected readonly BetBll betBll;

        public RouletteController(RouletteBll rouletteBll, BetBll betBll)
        {
            this.rouletteBll = rouletteBll;
            this.betBll = betBll;
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = rouletteBll.GetAll();
            return StatusCode((int)response.Message.Status, response);
        }

        [Route("/{id}")]
        [HttpGet("{id}")]
        public IActionResult GetRoulette(int id = 0)
        {
            var response = rouletteBll.GetById(id);
            return StatusCode((int)response.Message.Status, response);
        }

        [Route("new")]
        [HttpPost]
        public IActionResult Create()
        {
            var response = rouletteBll.Create();
            return StatusCode((int)response.Message.Status, response);
        }

        [Route("activate/{id}")]
        [HttpPut]
        public IActionResult Update(int id = 0)
        {
            var response = rouletteBll.Update(id, true);
            return StatusCode((int)response.Message.Status, response);
        }

        [Route("close/{id}")]
        [HttpPut]
        public IActionResult Close(int id = 0)
        {
            var response = rouletteBll.Close(id, false);
            return StatusCode((int)response.Message.Status, response);
        }

        [Route("bet")]
        [HttpPost]
        public IActionResult Post([FromBody] Bet bet)
        {
            var userid = Request.Headers["userid"];

            var response = betBll.RunBet(bet, userid);
            return StatusCode((int)response.Message.Status, response);
        }
    }
}
