
using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;


namespace AppStore.Repositories.Implementation
{
    public class LibroService : ILibroService
    {
        private readonly DatabaseContext ctx;
        //! Inyectando el contexto de la base de datos
        public LibroService(DatabaseContext ctxParameter)
        {
            ctx = ctxParameter;
        }
        //! Implementando metodo agregar
        public bool Add(Libro libro)
        {
            try
            {
                ctx.Libros!.Add(libro);
                ctx.SaveChanges();
                foreach(int categoriaId in libro.Categorias!)
                {
                   var libroCategoria = new LibroCategoria
                   {
                    LibroId = libro.Id,
                    CategoriaId = categoriaId
                   } ;
                   ctx.LibroCategorias!.Add(libroCategoria);
                }
                ctx.SaveChanges();
                return true;

            }
            catch(Exception )
            {
                return false;
            }
        }
        //! implementando metodo borrar
        public bool Delete(int id)
        {
            try
            {
                var data = GetById(id);
                if(data is null)
                {
                    return false;
                }
                var libroCategorias = ctx.LibroCategorias!.Where(a=>a.LibroId ==data.Id);
                ctx.LibroCategorias!.RemoveRange(libroCategorias);
                ctx.Libros!.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        //! implementando metodo obtener por id
        public Libro GetById(int id)
        {
            return ctx.Libros!.Find(id)!;

        }
        //! implementando metodo listar
        public LibroListVm List(string term="", bool paging=false, int currentPage =0)
        {
            var data = new LibroListVm();
            var list = ctx.Libros!.ToList();

            if(string.IsNullOrEmpty(term))
            {
                term= term.ToLower();
                list = list.Where(a=>a.Titulo!.ToLower().StartsWith(term)).ToList();
            }
            if(paging)
            {
                int pageSize = 5;
                int count = list.Count;
                int totalPages= (int)Math.Ceiling(count/(double)pageSize);
                list=list.Skip((currentPage-1)*pageSize).Take(pageSize).ToList();
                data.PageSize=pageSize;
                data.CurrentPage= currentPage;
                data.TotalPages = totalPages;
            }
            foreach(var libro in list)
            {
                var categorias = (
                    from Categoria in ctx.Categorias
                    join lc in ctx.LibroCategorias!
                    on Categoria.Id equals lc.CategoriaId
                    where lc.LibroId == libro.Id
                    select Categoria.Nombre
                ).ToList();
                string categoriaNombres = string.Join(",", categorias); 
                libro.CategoriasNames = categoriaNombres;
            }
            data.LibroList= list.AsQueryable();

            return data;
        }
        //! implementando metodo actualizar
        public bool Update(Libro libro)
        {
            try
            {
                var categoriasParaEliminar = ctx.LibroCategorias!.Where(a=>a.LibroId == libro.Id);
                foreach(var categoria in categoriasParaEliminar)
                {
                    ctx.LibroCategorias!.Remove(categoria);

                }
                foreach(int categoriaId in libro.Categorias!)
                {
                    var libroCategoria = new LibroCategoria{CategoriaId = categoriaId, LibroId = libro.Id};
                    ctx.LibroCategorias!.Add(libroCategoria);

                }
                ctx.Libros!.Update(libro);
                ctx.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        //! implementando metodo obtener categorias por libro id
        public List<int>GetCategoriaByLibroId(int LibroId)
        {
            return ctx.LibroCategorias!.Where(a=>a.LibroId == LibroId).Select(a=>a.CategoriaId).ToList();
        }
    }
}