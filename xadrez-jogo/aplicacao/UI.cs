using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_jogo.xadrez;

namespace xadrez_jogo.aplicacao
{
    public class UI
    {
        public static readonly string ANSI_RESET = "\u001B[0m";
        public static readonly string ANSI_BLACK = "\u001B[30m";
        public static readonly string ANSI_RED = "\u001B[31m";
        public static readonly string ANSI_GREEN = "\u001B[32m";
        public static readonly string ANSI_YELLOW = "\u001B[33m";
        public static readonly string ANSI_BLUE = "\u001B[34m";
        public static readonly string ANSI_PURPLE = "\u001B[35m";
        public static readonly string ANSI_CYAN = "\u001B[36m";
        public static readonly string ANSI_WHITE = "\u001B[37m";
        // FUNDO CORES
        public static readonly string ANSI_BLACK_BACKGROUND = "\u001B[40m";
        public static readonly string ANSI_RED_BACKGROUND = "\u001B[41m";
        public static readonly string ANSI_GREEN_BACKGROUND = "\u001B[42m";
        public static readonly string ANSI_YELLOW_BACKGROUND = "\u001B[43m";
        public static readonly string ANSI_BLUE_BACKGROUND = "\u001B[44m";
        public static readonly string ANSI_PURPLE_BACKGROUND = "\u001B[45m";
        public static readonly string ANSI_CYAN_BACKGROUND = "\u001B[46m";
        public static readonly string ANSI_WHITE_BACKGROUND = "\u001B[47m";

        public static void LimparTela()
        {
            Console.Clear();
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            try
            {
                string s = Console.ReadLine();
                char coluna = s[0];
                int linha = int.Parse(s.Substring(1));
                return new PosicaoXadrez(coluna, linha);
            }
            catch (Exception)
            {
                throw new FormatException("Erro ao ler a posição de xadrez. Valores válidos são de a1 até h8.");
            }
        }

        public static void ImprimirTabuleiro(PecaXadrez[,] pecas)
        {
            ImprimirBordaTabuleiro();
            for (int i = 0; i < pecas.GetLength(0); i++)
            {
                Console.Write((8 - i) + " |");
                for (int j = 0; j < pecas.GetLength(1); j++)
                {
                    ImprimirPeca(pecas[i, j], false);
                }
                Console.WriteLine("|");
            }
            ImprimirBordaTabuleiro();
            Console.WriteLine("   a b c d e f g h");
        }

        public static void ImprimirPartida(PartidaXadrez partida, List<PecaXadrez> capturadas)
        {
            ImprimirCabecalho();
            ImprimirTabuleiro(partida.Pecas);
            Console.WriteLine();
            ImprimirPecasCapturadas(capturadas);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.turno);
            if (!partida.XequeMate)
            {
                Console.WriteLine("Aguardando jogador: " + partida.JogadorAtual);
                if (partida.Xeque)
                {
                    Console.WriteLine("!! XEQUE !!");
                }
            }
            else
            {
                Console.WriteLine("!!!! XEQUEMATE !!!!");
                Console.WriteLine("Vencedor: " + partida.JogadorAtual);
            }
        }

        public static void ImprimirTabuleiro(PecaXadrez[,] pecas, bool[,] movimentosPossiveis)
        {
            ImprimirBordaTabuleiro();
            for (int i = 0; i < pecas.GetLength(0); i++)
            {
                Console.Write((8 - i) + " |");
                for (int j = 0; j < pecas.GetLength(1); j++)
                {
                    ImprimirPeca(pecas[i, j], movimentosPossiveis[i, j]);
                }
                Console.WriteLine("|");
            }
            ImprimirBordaTabuleiro();
            Console.WriteLine("   a b c d e f g h");
        }

        private static void ImprimirPeca(PecaXadrez peca, bool fundo)
        {
            if (fundo)
            {
                Console.Write(ANSI_BLUE_BACKGROUND);
            }
            if (peca == null)
            {
                Console.Write("-" + ANSI_RESET);
            }
            else
            {
                if (peca.Cor == Cor.Branco)
                {
                    Console.Write(ANSI_WHITE + peca + ANSI_RESET);
                }
                else
                {
                    Console.Write(ANSI_YELLOW + peca + ANSI_RESET);
                }
            }
            Console.Write(" ");
        }
        public static void BancoDeDados()
        {
            using var context = new ChessDbContext();
            try
            {
                Console.WriteLine("Verificando se o banco de dados está criado...");
                context.Database.EnsureCreated(); // Cria o banco de dados se não existir

                Console.WriteLine("Banco de dados verificado/ criado com sucesso.");

                // Adicionar uma nova partida como exemplo
                var novaPartida = new PartidaXadrez
                {
                    // Inicializar propriedades da partida, se necessário
                };
                context.PartidasXadrez.Add(novaPartida);
                context.SaveChanges();
                Console.WriteLine("Partida criada com sucesso no banco de dados.");

                // Ler partidas existentes
                var partidas = context.PartidasXadrez.ToList();
                Console.WriteLine($"Total de partidas no banco de dados: {partidas.Count}");
            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar ou manipular o banco de dados: {ex.Message}");

            }
        }   
        private static void ImprimirPecasCapturadas(List<PecaXadrez> capturadas)
        {
            List<PecaXadrez> brancas = capturadas.Where(x => x.Cor == Cor.Branco).ToList();
            List<PecaXadrez> pretas = capturadas.Where(x => x.Cor == Cor.Preto).ToList();
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            Console.Write(ANSI_WHITE);
            Console.WriteLine(string.Join(", ", brancas));
            Console.Write(ANSI_RESET);
            Console.Write("Pretas: ");
            Console.Write(ANSI_YELLOW);
            Console.WriteLine(string.Join(", ", pretas));
            Console.Write(ANSI_RESET);
        }

        public static void ImprimirBordaTabuleiro()
        {
            Console.WriteLine("  +----------------+");
        }

        public static void ImprimirCabecalho()
        {
            Console.WriteLine(ANSI_CYAN + "============================         D -> Rainha ");
            Console.WriteLine("|       JOGO DE XADREZ      |        ");
            Console.WriteLine("============================        " + ANSI_RESET);
        }
    }
}
