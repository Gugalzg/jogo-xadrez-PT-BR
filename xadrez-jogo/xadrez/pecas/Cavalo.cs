using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_jogo.tabuleiroJogo;


namespace xadrez_jogo.xadrez.pecas
{
    public class Cavalo : PecaXadrez
    {
        public int Id { get; set; }
        
        public Cavalo(){}
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }

        public override string ToString()
        {
            return "C";
        }

        private bool PodeMover(Posicao posicao)
        {
            PecaXadrez p = (PecaXadrez)ObterTabuleiro().Peca(posicao);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[ObterTabuleiro().Linhas(), ObterTabuleiro().Colunas()];
            Posicao pos = new Posicao(0, 0);

            // Acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Abaixo
            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Esquerda
            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Direita
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // NO
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // NE
            pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // SO
            pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // SE
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (ObterTabuleiro().PosicaoExiste(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            return mat;
        }
    }
}
