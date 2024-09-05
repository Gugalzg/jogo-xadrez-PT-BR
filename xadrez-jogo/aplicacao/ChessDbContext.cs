using Microsoft.EntityFrameworkCore;
using xadrez_jogo.tabuleiroJogo;
using xadrez_jogo.xadrez;
using xadrez_jogo.xadrez.pecas;

namespace xadrez_jogo.aplicacao
{
    public class ChessDbContext : DbContext
    {
        // DbSet para cada entidade
        public DbSet<PartidaXadrez> PartidasXadrez { get; set; }
        public DbSet<PecaXadrez> PecasXadrez { get; set; }
        public DbSet<Tabuleiro> Tabuleiros { get; set; }

        // Configuração do banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configurar a string de conexão com o banco de dados
            // Ajuste conforme seu banco de dados (SQLite, SQL Server, etc.)
            optionsBuilder.UseSqlite("Data Source=ChessGameDb.db");
        }

        // Configuração das entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da entidade PartidaXadrez
            modelBuilder.Entity<PartidaXadrez>()
                .HasKey(p => p.Id);
            
            // Configuração da entidade PecaXadrez
            modelBuilder.Entity<PecaXadrez>()
                .HasKey(p => p.Id);

            // Configuração da entidade Tabuleiro
            modelBuilder.Entity<Tabuleiro>()
                .HasKey(t => t.Id);
            
            // Configuração para a herança de PecaXadrez
            modelBuilder.Entity<PecaXadrez>()
                .HasDiscriminator<string>("TipoPeca")
                .HasValue<Bispo>("Bispo")
                .HasValue<Peao>("Peao")
                .HasValue<Rainha>("Rainha")
                .HasValue<Torre>("Torre")
                .HasValue<Cavalo>("Cavalo")
                .HasValue<Rei>("Rei");

            // Configuração de relacionamentos
            modelBuilder.Entity<PartidaXadrez>()
                .HasMany(p => p.PecasXadrez)
                .WithOne() // Assumindo que não há uma propriedade de navegação inversa
                .HasForeignKey("PartidaXadrezId"); // Ajuste a chave estrangeira conforme necessário

            // Outras configurações necessárias
            // Exemplo: Configuração de conversão de tipo
            modelBuilder.Entity<PecaXadrez>()
                .Property(p => p.Cor)
                .HasConversion<string>();
        }
    }
}
