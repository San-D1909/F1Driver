using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1DriverBack.Controllers
{
    public class CircuitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
