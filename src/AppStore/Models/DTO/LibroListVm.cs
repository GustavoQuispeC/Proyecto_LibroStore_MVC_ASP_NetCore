using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppStore.Models.Domain;

namespace AppStore.Models.DTO
{
    public class LibroListVm
    {
        public IQueryable<Libro>? LIbroList{get; set;}
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }
}