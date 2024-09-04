using xadrez_jogo.xadrez;

namespace xadrez_jogo.aplicacao
{
    public class Program
    {
        static void Main(string[] args)
        {
            var partidaXadrez = new PartidaXadrez();
            var capturadas = new List<PecaXadrez>();

            while (!partidaXadrez.XequeMate)
            {
                try
                {
                    UI.LimparTela();
                    UI.ImprimirPartida(partidaXadrez, capturadas);
                    Console.WriteLine();
                    Console.Write("Origem: ");
                    PosicaoXadrez origem = UI.LerPosicaoXadrez();

                    var movimentosPossiveis = partidaXadrez.MovimentosPossiveis(origem);
                    UI.LimparTela();
                    UI.ImprimirCabecalho();
                    UI.ImprimirTabuleiro(partidaXadrez.Pecas, movimentosPossiveis);

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    PosicaoXadrez destino = UI.LerPosicaoXadrez();

                    var pecaCapturada = partidaXadrez.ExecutarMovimentoXadrez(origem, destino);

                    if (pecaCapturada != null)
                    {
                        capturadas.Add(pecaCapturada);
                    }

                    if (partidaXadrez.Promovida != null)
                    {
                        Console.Write("Digite a peça para promoção (B/C/T/D): ");
                        var tipo = Console.ReadLine().ToUpper();
                        while (tipo != "B" && tipo != "C" && tipo != "T" && tipo != "D")
                        {
                            Console.Write("Valor inválido! Digite a peça para promoção (B/C/T/D): ");
                            tipo = Console.ReadLine().ToUpper();
                        }
                        partidaXadrez.SubstituirPecaPromovida(tipo);
                    }
                }
                catch (XadrezException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
                
            }

            UI.LimparTela();
            UI.ImprimirCabecalho();
            UI.ImprimirPartida(partidaXadrez, capturadas);
        }
    }
}

