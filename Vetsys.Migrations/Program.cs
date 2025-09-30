using Vetsys.API.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<VetsysDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("Vetsys.Migrations")
    )
);

var host = builder.Build();

// Aplica migraciones automáticamente al iniciar el Worker
using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VetsysDbContext>();
    db.Database.Migrate();
}

host.Run();
