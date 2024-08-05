using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;

namespace AppStore.Repositories.Implementation
{
    public class CategoriaService : ICategoriaService
    {
        private readonly DatabaseContext ctx;

        public CategoriaService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public IQueryable<Categoria> List()
        {
            return ctx.Categorias!.AsQueryable();
        }
         public bool Add(Categoria categoria)
        {
            try
            {
                ctx.Categorias!.Add(categoria);
                ctx.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                
               return false;
            }
        }

        public bool Update(Categoria categoria)
        {
            try
            {
                var data = GetById(categoria.Id);
                if(data is null)
                {
                    return false;
                }
                data.Nombre = categoria.Nombre;
                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
        }

        public Categoria GetById(int id)
        {
           return ctx.Categorias!.Find(id)!;
        }

        public bool Delete(int id)
        {
            try
            {
                var data = GetById(id);
                if(data is null)
                {
                    return false;
                }
                ctx.Categorias!.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                
                return false;
            }
        }

        public CategoriaList List(Categoria categoria)
        {
            var data = new CategoriaList();
            var list = ctx.Categorias!.ToList();
            data.CategoriaLista = list.AsQueryable();
            return data;
        }
    }
}