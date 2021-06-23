using Microsoft.AspNetCore.Mvc;
using Msdi.WebApi.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Msdi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
