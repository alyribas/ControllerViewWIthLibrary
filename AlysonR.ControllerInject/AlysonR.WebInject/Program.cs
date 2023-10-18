using AlysonR.ControllerLibrary.Controllers;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Captura o Assumbly do controller
Assembly inicio = typeof(InicioController).Assembly;

// Configura os Controllers e Views e em seguida adiciona o assembly do controller que está na library.
builder.Services.AddControllersWithViews()
    .AddApplicationPart(inicio);

// Configuração para encontrar o path das views
builder.Services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
{ 
    options.FileProviders.Add(new EmbeddedFileProvider(inicio));
});

// Configuração para compilar as views em tempo de execução
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Mapeia as Views que foram importadas da library.
app.MapRazorPages();
app.Run();
