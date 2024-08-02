using AppStore.Models.Domain;
using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppStore.Controllers
{
    public class LibroController : Controller
    {
        //! inyectando servicios de libros, imagenes y categorias
        private readonly ILibroService _libroService;
        private readonly IFileService _fileService;
        private readonly ICategoriaService _categoriaService;
        public LibroController(ILibroService libroService, IFileService fileService, ICategoriaService categoriaService)
        {
        _libroService = libroService;
        _fileService = fileService;
        _categoriaService = categoriaService;
        }
//! controles API
        [HttpPost]
        public IActionResult Add(Libro libro)
        {
            libro.CategoriaList =  _categoriaService.List()
            .Select(x=> new SelectListItem(Text = x.Nombre, Value = x.Id.ToString()));
            if(!ModelState.IsValid)
            {
                return View(libro);
            }
            if(libro.ImagenFile != null)
            {
            var resultado = _fileService.SaveImage(libro.ImagenFile);
                if(resultado.Item1==0)
                {
                    TempData["msg"] = "la imagen no pudo guardarse exitosamente";
                    return View(libro);
                }
                var imagenName = resultado.Item2;
                libro.Imagen = imagenName;

            }
            var resultadoLibro = _libroService.Add(libro);
            if(resultadoLibro)
            {
                TempData["msg"] = "El libro ser guardado exitosamente";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error al guardar el libro";
            return View(libro);
            

        }

//! controles de navegacion
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        public IActionResult LibroList()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            return RedirectToAction(nameof(LibroList));
        }
    }
}