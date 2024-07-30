using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AppStore.Models.Domain
{
    public class LoadDatabase
    {
        public static async Task InsertarData(DatabaseContext context, UserManager<ApplicationUser>usuarioManager, RoleManager<IdentityRole>rolManager)
        {
            //!agregando usuario por default siempre que no exista
            if(!rolManager.Roles.Any())
            {
                await rolManager.CreateAsync(new IdentityRole("ADMIN"));
            }
            if(!usuarioManager.Users.Any()){
                var usuario = new ApplicationUser{
                    Nombre = "Gustavo Quispe",
                    Email="gusstavocta@gmail.com",
                    UserName ="gustavo.quispe"
                };
                //! password
                await usuarioManager.CreateAsync(usuario, "123456");
                //! role
                await usuarioManager.AddToRoleAsync(usuario,"ADMIN");
            }

            //! agregando categoria por default siempre que no exista
            if(!context.Categorias!.Any()){
                context.Categorias!.AddRange(
                    new Categoria{Nombre = "Drama"},
                    new Categoria{Nombre = "Coemdia"},
                    new Categoria{Nombre = "Accion"},
                    new Categoria{Nombre = "Terror"},
                    new Categoria{Nombre = "Aventura"}
                );
            }

            //! agregando Libros por default siempre que no exista
            if(!context.Libros!.Any()){
                context.Libros!.AddRange(
                    new Libro{
                        Titulo = "El Quijote de la mancha",
                        CreateDate="12/01/2024",
                        Imagen = "quijote.jpg",
                        Autor = "Miguel de Cervantes"
                    },
                    new Libro{
                        Titulo = "Harry Potter",
                        CreateDate="11/01/2023",
                        Imagen = "harry.jpg",
                        Autor = "Juan de la Cruz"
                    }
                );
            }
            //! agregando LibroCategorias por default siempre que no exista
            if(!context.LibroCategorias!.Any()){
                context.LibroCategorias!.AddRange(
                    new LibroCategoria{CategoriaId = 1, LibroId = 1},
                    new LibroCategoria{CategoriaId = 2, LibroId = 2}

                );
            }
            context.SaveChanges();


        }
    }
}