using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xadrez_jogo.tabuleiroJogo
{
    public abstract class Peca
    {
         public int Id { get; set; } 
        public Posicao Posicao { get; set; }
        public Tabuleiro Tabuleiro;

        public Peca(){ }
        protected Peca(Tabuleiro tabuleiro)
        {
            Tabuleiro = tabuleiro;
            Posicao = null;
        }

        protected Tabuleiro ObterTabuleiro()
        {
            return Tabuleiro;
        }

        public abstract bool[,] MovimentosPossiveis();

        public bool MovimentoPossivel(Posicao posicao)
        {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }

        public bool ExisteAlgumMovimentoPossivel()
        {
            bool[,] matriz = MovimentosPossiveis();
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

