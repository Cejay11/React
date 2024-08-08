using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JuevesanoDapperAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class sampleController : ControllerBase
    {
        public sampleController()
        {
            
        }

        [HttpGet("WhoAmI")]
        public IActionResult WhoAmI()
        {
            return Ok("I am Crisjay");
        }

         [HttpPost("NiceToMeetYou")]
        public IActionResult NiceToMeetYou(string yourName)
        {
           if (string.IsNullOrEmpty(yourName))
           {
            return BadRequest("i need your name");
           }
           return Ok($"Hello {yourName}");
        }
    }
}