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

        //! Inyectando el servicio de libros
        public HomeController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        //! Método que se ejecuta al cargar la vista
        public IActionResult Index(string term ="", int currentPage = 1)
        {
            var libroListVm = _libroService.List(term, true, currentPage);
            return View(libroListVm);
        }

    }
}