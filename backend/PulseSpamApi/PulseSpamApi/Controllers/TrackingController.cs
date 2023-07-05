using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseSpamApi.Interfaces;
using PulseSpamApi.Models;
using PulseSpamApi.Services;

namespace PulseSpamApi.Controllers
{
    [ApiController]
    [Route("api/trackings")]
    [Authorize]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingService _trackingService;

        public TrackingController(ITrackingService trackingService) =>
            _trackingService = trackingService;

        [HttpGet]
        public async Task<List<Tracking>> Get() =>
            await _trackingService.GetAsync();

        [HttpGet("usuarios/{usuario}/{fecha}")]
        public async Task<ActionResult<Tracking>> GetUserDate(string usuario, string fecha) {
            DateTime date = DateTime.Parse(fecha);
            var t = await _trackingService.GetAsyncUserDate(usuario, date);
            if (t != null)
            {
                return t;
            } else
            {
                return NotFound();
            }
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Tracking>> Get(string id)
        {
            var tracking = await _trackingService.GetAsync(id);

            if (tracking is null)
            {
                return NotFound();
            }

            return tracking;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Tracking nuevoTracking)
        {
            await _trackingService.CreateAsync(nuevoTracking);

            return CreatedAtAction(nameof(Get), new { id = nuevoTracking.Id }, nuevoTracking);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Tracking updTracking)
        {
            var tracking = await _trackingService.GetAsync(id);

            if (tracking is null)
            {
                return NotFound();
            }

            updTracking.Id = tracking.Id;

            await _trackingService.UpdateAsync(id, updTracking);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var tracking = await _trackingService.GetAsync(id);

            if (tracking is null)
            {
                return NotFound();
            }

            await _trackingService.RemoveAsync(id);

            return NoContent();
        }
    }
}
