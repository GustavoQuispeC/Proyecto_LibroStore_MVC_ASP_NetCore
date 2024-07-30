using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppStore.Models.Domain
{
    public class Libro
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? CreateDate { get; set; }
        public string? Imagen { get; set; }
        [Required]
        public string? Autor { get; set; }
        public virtual ICollection<Categoria>?CategoriaRelationList{get; set;}
        public virtual ICollection<LibroCategoria>?LibroCategoriaRelationList{get; set;}

    }
}