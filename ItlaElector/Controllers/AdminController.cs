﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ItlaElector.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }

      
    }
}