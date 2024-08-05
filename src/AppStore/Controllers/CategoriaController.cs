using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AppStore.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService) 
        {
            _categoriaService = categoriaService;
        }

        //! Control API
        [HttpPost]
        public IActionResult Add(Categoria categoria)
        {
           if (!ModelState.IsValid)
            {
                return View(categoria);
            }
           var resultadoCategoria = _categoriaService.Add(categoria);
            if(!resultadoCategoria)
            {
                TempData["msg"] = "La categoria se guardo correctamente";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error al guardar la categoria";
            return View(categoria);
        }

        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {
            var categoriaData = _categoriaService.GetById(categoria.Id);
            
            categoriaData.Nombre = categoria.Nombre;
            var resultadoCategoria = _categoriaService.Update(categoriaData);
            if(!resultadoCategoria)
            {
                TempData["msg"] = "Error al actualizar la categoria";
                return View(categoria);
            }
            TempData["msg"] = "La categoria se actualizo correctamente";
            return RedirectToAction(nameof(CategoriaList));

        }   




        //Control de Navegacion
        public IActionResult Add()
        {
            var categoria = new Categoria();
            return View(categoria);
        }

        public IActionResult Edit(int id)
        {
            var categoria = _categoriaService.GetById(id);

            return View(categoria);
            
        }

        public IActionResult CategoriaList()
        {
            var categorias = _categoriaService.List().ToList();
            var categoriaList = new CategoriaList { CategoriaLista = categorias.AsQueryable() };
            return View(categoriaList);
        }

        public IActionResult Delete(int id)
        {
            _categoriaService.Delete(id);
            return RedirectToAction(nameof(CategoriaList));
        }
       
    }
}
