using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppStore.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILibroService _libroService;

        public HomeController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        public IActionResult Index(string term ="", int currentPage = 1)
        {
            var libros = _libroService.List(term, true, currentPage);
            return View(libros);
        }

    }
}