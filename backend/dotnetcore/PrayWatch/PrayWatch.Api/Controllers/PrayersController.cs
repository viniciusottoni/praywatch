using Microsoft.AspNetCore.Mvc;
using PrayWatch.Domain.Interfaces;
using PrayWatch.Domain.Models;
using System.Collections.Generic;

namespace PrayWatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrayersController : ControllerBase
    {
        private readonly IPrayersService _prayerService;

        public PrayersController(IPrayersService prayerService)
        {
            _prayerService = prayerService;
        }

        [HttpGet("list")]
        public ActionResult<List<PrayerModel>> GetAllBy(string purpose)
        {
            return Ok(_prayerService.GetAllBy(purpose));
        }
    }
}
