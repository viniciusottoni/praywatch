using Microsoft.AspNetCore.Mvc;
using PrayWatch.Domain.Interfaces;
using PrayWatch.Domain.Models;
using System.Collections.Generic;

namespace PrayWatch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurposesController : ControllerBase
    {
        private readonly IPurposesService _purposesService;

        public PurposesController(IPurposesService purposesService)
        {
            _purposesService = purposesService;
        }

        [HttpGet("praywatch")]
        public ActionResult<PurposeModel> GetBy(string purpose)
        {
            return Ok(_purposesService.GetBy(purpose));
        }
    }
}