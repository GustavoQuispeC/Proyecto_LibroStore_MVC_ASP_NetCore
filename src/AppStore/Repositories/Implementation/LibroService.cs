using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;

namespace AppStore.Repositories.Implementation
{
    public class LibroService : ILibroService
    {
        public bool Add(Libro libro)
        {
            return false;
        }

        public bool Delete(int id)
        {
            return false;
        }

        public Libro GetById(int id)
        {
            return null!;

        }
        public LibroListVm List(string term="", bool paging=false, int currentPage =0)
        {
            return null!;
        }

        public bool Update(Libro libro)
        {
            return false;
        }
        public List<int>GetCategoriaByLibroId(int LibroId)
        {
            return null!;
        }
    }
}