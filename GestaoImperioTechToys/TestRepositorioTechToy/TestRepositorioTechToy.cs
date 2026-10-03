using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechToysDominio;

[TestClass]
    public class TestRepositorioTechToy
    {
        public partial class MyDBContext : DbContext
        {
            public DbSet<Impressora> Impressora { get; set; }
            public DbSet<Fornecedor> Fornecedor { get; set; }
            public DbSet<MateriaPrima> MateriaPrima{ get; set; }
            public DbSet<Cliente> Cliente { get; set; }
            public DbSet<Produto> Produto { get; set; }
            public DbSet<Embalagem> Embalagem { get; set; }
            public DbSet<Venda> Venda { get; set; }

            static MyDBContext()
            {
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }
            public MyDBContext()
            {
                Database.EnsureCreated();
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                base.OnConfiguring(optionsBuilder);
                var server = "localhost";
                var port = "5432";
                var username = "postgres";
                var password = "admin";
                var database = "TechToys";
                var conStr = $"Host={server};Port={port};Database={database};Username={username};Password={password}";

                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseNpgsql(conStr);
                }
            }
        }

        [TestMethod]
        public void CriarBanco()
        {
            using (var db = new MyDBContext())
            {
                Assert.IsNotNull(db);
            }
        }

        [TestMethod]
        public void InsertImpressora()
        {
            using (var db = new MyDBContext())
            {
                var impressora = new Impressora() { Nome = "Principal", Kwh = 0.15, Modelo="Creality Ender 3 v3"};
                db.Impressora.Add(impressora);
                
                db.SaveChanges();
            }
        }

        [TestMethod]
        public void InsertFornecedor()
        {
            using (var db = new MyDBContext())
            {
                var fornecedor = new Fornecedor() { Nome = "Benser", Site="www.benser.com"};
                db.Fornecedor.Add(fornecedor);

                fornecedor = new Fornecedor() { Nome = "LAZAPACK", Site = "https://www.mercadolivre.com.br/100-mini-sacolas-papel-kraft-presente-lembrancinhas-12x85x16cm/p/MLB29540395?pdp_filters=item_id%3AMLB4779479101&from=gshop&matt_tool=40300352&matt_word=&matt_source=google&matt_campaign_id=22090354013&matt_ad_group_id=193694086727&matt_match_type=&matt_network=g&matt_device=c&matt_creative=787871502227&matt_keyword=&matt_ad_position=&matt_ad_type=pla&matt_merchant_id=735125422&matt_product_id=MLB29540395-product&matt_product_partition_id=2495838816379&matt_target_id=pla-2495838816379&cq_src=google_ads&cq_cmp=22090354013&cq_net=g&cq_plt=gp&cq_med=pla&gad_source=1&gad_campaignid=22090354013&gclid=EAIaIQobChMI5JzX8OGZlwMViElIAB3vBRIAEAQYASABEgLtE_D_BwE" };
                db.Fornecedor.Add(fornecedor);
                db.SaveChanges();
            }
        }

        [TestMethod]
        public void InsertCliente()
        {
            using (var db = new MyDBContext())
            {
                var cliente = new Cliente() { Nome = "jonas", Instagram = "jonas.brothers" } ;
                db.Cliente.Add(cliente);

                db.SaveChanges();
            }
        }

        [TestMethod]
        public void InsertMateriaPrima()
        {
            using (var db = new MyDBContext())
            {
                var fornecedor = db.Fornecedor.FirstOrDefault(x => x.Nome == "Benser");

                var materia_prima = new MateriaPrima() { Nome="filamento branco", Fornecedor = fornecedor, Peso_compra = 1000, Preco_compra = 70 };
                db.MateriaPrima.Add(materia_prima);

                db.SaveChanges();
            }
        }

        [TestMethod]
        public void InsertProduto()
        {
            using (var db = new MyDBContext())
            {
                var materia_prima = db.MateriaPrima.FirstOrDefault(x => x.Id == 1);
                var impressora = db.Impressora.FirstOrDefault(x=> x.Id == 1);

                var produto = new Produto() { Materia_Prima = materia_prima, Dimensoes = (3, 15, 4), Impressora = impressora, Nome="Lagartixa", Peso_Impressao = 13, Tempo_Impressao = 90, Valor_Venda = 20};
                db.Produto.Add(produto);

                db.SaveChanges();
            }
        }

        [TestMethod]
        public void InsertEmbalagem()
        {
            using (var db = new MyDBContext())
            {
                var fornecedor = db.Fornecedor.FirstOrDefault(x => x.Nome == "Benser");

                var embalagem = new Embalagem() { Fornecedor = fornecedor, Dimensoes = (10, 20, 10), Estoque = 5, Nome = "Caixa Pequena", Preco = 3.00, Tipo = "Caixa"};
                db.Embalagem.Add(embalagem);

                fornecedor = db.Fornecedor.FirstOrDefault(x => x.Nome == "LAZAPACK");
                embalagem = new Embalagem() { Fornecedor = fornecedor, Dimensoes = (12, 8.5, 16), Estoque = 5, Nome = "Sacola de Papelão", Preco = 0.7, Tipo = "Sacola" };
                db.Embalagem.Add(embalagem);

                db.SaveChanges();
            }
        }

        [TestMethod]
        public void InsertVenda()
        {
            using (var db = new MyDBContext())
            {
                var cliente = db.Cliente.FirstOrDefault(x => x.Nome == "jonas");
                var embalagem = db.Embalagem.FirstOrDefault(x => x.Nome == "Caixa Pequena");
                var produto = db.Produto.FirstOrDefault(x => x.Nome == "Lagartixa");


                var venda = new Venda() { Cliente = cliente, Data_Hora = DateTime.Now, Embalagem = embalagem, Produto = produto, Quantidade = 1 };
                db.Venda.Add(venda);

                db.SaveChanges();
            }
        }
    }
}

