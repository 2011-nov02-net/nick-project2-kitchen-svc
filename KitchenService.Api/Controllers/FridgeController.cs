using System.Collections.Generic;
using System.Linq;
using KitchenService.Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitchenService.Api.Controllers
{
    [Route("api/appliances/fridge")]
    [ApiController]
    public class FridgeController : ControllerBase
    {
        private readonly Fridge _fridge;

        public FridgeController(Fridge fridge)
        {
            _fridge = fridge;
        }

        // GET /api/appliances/fridge
        [HttpGet]
        [ProducesResponseType(typeof(Appliance), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var appliance = new Appliance { Id = _fridge.Id, Name = _fridge.Name };
            return Ok(appliance);
        }

        // GET /api/appliances/fridge/contents
        [HttpGet("contents")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<FridgeItem>), StatusCodes.Status200OK)]
        public IActionResult GetContents()
        {
            return Ok(_fridge.Contents);
        }

        // POST /api/appliances/fridge/contents
        [HttpPost("contents")]
        [ProducesResponseType(typeof(FridgeItem), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostContents(FridgeItem item)
        {
            _fridge.AddItem(item);
            return CreatedAtAction(
                actionName: nameof(GetContentsById),
                routeValues: new { item.Id },
                value: item);
        }

        // GET /api/appliances/fridge/contents/5
        [HttpGet("contents/{id}")]
        [ProducesResponseType(typeof(FridgeItem), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetContentsById(int id)
        {
            if (_fridge.Contents.FirstOrDefault(x => x.Id == id) is FridgeItem item)
            {
                return Ok(item);
            }
            return NotFound();
        }
    }
}
