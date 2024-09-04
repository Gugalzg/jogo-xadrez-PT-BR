using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_jogo.tabuleiroJogo;

namespace xadrez_jogo.xadrez
{
    public class PosicaoXadrez
    {
        public char Coluna { get; }
        public int Linha { get; }

        public PosicaoXadrez(char coluna, int linha)
        {
            if (coluna < 'a' || coluna > 'h' || linha < 1 || linha > 8)
            {
                throw new ArgumentException("Erro ao instanciar PosicaoXadrez. Valores válidos são de a1 a h8.");
            }
            Coluna = coluna;
            Linha = linha;
        }

        public Posicao ParaPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public static PosicaoXadrez DePosicao(Posicao posicao)
        {
            return new PosicaoXadrez((char)('a' + posicao.Coluna), 8 - posicao.Linha);
        }

        public override string ToString()
        {
            return $"{Coluna}{Linha}";
        }

    }
}
