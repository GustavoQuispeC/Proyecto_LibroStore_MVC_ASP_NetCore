using AppStore.Models.Domain;
using AppStore.Repositories.Abstract;
using AppStore.Repositories.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//! creando objeto desde la interfaz y servicio
builder.Services.AddScoped<ILibroService, LibroService>();

//! Add the DatabaseContext to the services container and configure it to use the Sqlite database.
builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    opt.LogTo(Console.WriteLine, new[]{
        DbLoggerCategory.Database.Command.Name},
        LogLevel.Information).EnableSensitiveDataLogging();
        opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteDataBase"));
});
//! Add the Identity services to the services container.
builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
.AddEntityFrameworkStores<DatabaseContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//! Add the authentication and authorization middleware to the request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//! preparando insercion de data por default
using (var ambiente = app.Services.CreateScope())
{
    var services = ambiente.ServiceProvider;
    try{
        var context = services.GetRequiredService<DatabaseContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    
    await context.Database.MigrateAsync();
    await LoadDatabase.InsertarData(context,userManager,roleManager);


    }
    catch(Exception e)
    {
        //! MOstrando error en console
        var logging = services.GetRequiredService<ILogger<Program>>();
        logging.LogError(e, "Error en la migracion de datos");
    }
   
}    
app.Run();
