using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppStore.Models.Domain
{
    public class LibroCategoria
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria {get; set;} //Ancla
        public int LibroId { get; set; }
        public Libro? Libro {get; set;} //Ancla
    }
}