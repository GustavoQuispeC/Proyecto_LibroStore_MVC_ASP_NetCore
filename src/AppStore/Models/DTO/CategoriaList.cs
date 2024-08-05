using AppStore.Models.Domain;

namespace AppStore.Models.DTO
{
    public class CategoriaList
    {
        public IQueryable<Categoria>? CategoriaLista { get; set; }
    }
}
