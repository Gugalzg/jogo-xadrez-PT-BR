using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_jogo.tabuleiroJogo;

namespace xadrez_jogo.xadrez.pecas
{
    public class Rei : PecaXadrez
    {
        public int Id { get; set; }
        private PartidaXadrez partidaXadrez;  
        public Rei(){}
        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaXadrez partidaXadrez) : base(tabuleiro, cor)
        {
            this.partidaXadrez = partidaXadrez;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool PodeMover(Posicao posicao)
        {
            PecaXadrez pos = (PecaXadrez)ObterTabuleiro().Peca(posicao);
            return pos == null || pos.Cor != Cor;
        }

        private bool TestaTorreRoque(Posicao posicao)
        {
            PecaXadrez pos = (PecaXadrez)ObterTabuleiro().Peca(posicao);
            return pos != null && pos is Torre && pos.Cor == Cor && pos.ContagemMovimentos == 0;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[ObterTabuleiro().Linhas(), ObterTabuleiro().Colunas()];
            Posicao pos = new Posicao(0, 0);

            // Acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // NO
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // NE
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // SO
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // SE
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Movimento especial: Roque
            if (ContagemMovimentos == 0 && !partidaXadrez.Xeque)
            {
                // Roque pequeno
                Posicao posT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TestaTorreRoque(posT1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    if (ObterTabuleiro().Peca(p1) == null && ObterTabuleiro().Peca(p2) == null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }

                // Roque grande
                Posicao posT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TestaTorreRoque(posT2))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    if (ObterTabuleiro().Peca(p1) == null && ObterTabuleiro().Peca(p2) == null && ObterTabuleiro().Peca(p3) == null)
                    {
                        mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}

