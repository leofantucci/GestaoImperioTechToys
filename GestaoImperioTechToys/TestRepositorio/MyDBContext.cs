using Microsoft.EntityFrameworkCore;
using TechToysDominio;

namespace TechToysDados
{
    public class MyDBContext : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Impressora> Impressora { get; set; }
        public DbSet<MateriaPrima> MateriaPrima { get; set; }
        public DbSet<Embalagem> Embalagem { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Venda> Venda { get; set; }

        static MyDBContext()
        {
            // Necessário para compatibilidade com DateTime do PostgreSQL
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conStr = "Host=localhost;Port=5432;Database=TechToys;Username=postgres;Password=admin";
                optionsBuilder.UseNpgsql(conStr);
            }
        }
    }
}