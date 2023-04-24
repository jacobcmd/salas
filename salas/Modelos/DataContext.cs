using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace salas.Modelos
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option)
        {

        }
        public DbSet<Salas> salas { get; set; }
        public DbSet<Reservaciones> reservaciones { get; set; }

    }
}
