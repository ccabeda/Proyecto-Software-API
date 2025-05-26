using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Software_Individual.Infraestructure.Data
{
    public class AplicationDbContextFactory : IDesignTimeDbContextFactory<AplicationDbContext> //clase para conexion a base de datos. Con API no usaba esto, tuve que googlear
    {
        public AplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<AplicationDbContext>() //uso los secret.json
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<AplicationDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Connection"));
            return new AplicationDbContext(optionsBuilder.Options);
        }
    }
}
