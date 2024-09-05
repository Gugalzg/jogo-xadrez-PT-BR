using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_jogo.tabuleiroJogo;

namespace xadrez_jogo.xadrez.pecas
{
    public class Rainha : PecaXadrez
    {
        public int Id { get; set; }
        
        public Rainha(){}
        public Rainha(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }

        public override string ToString()
        {
            return "D";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[ObterTabuleiro().Linhas(), ObterTabuleiro().Colunas()];
            Posicao pos = new Posicao(0, 0);

            // Acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (ObterTabuleiro().PosicaoExiste(pos) && !ObterTabuleiro().ExistePeca(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                pos.DefinirValores(pos.Linha - 1, pos.Coluna);
            }
            if (ObterTabuleiro().PosicaoExiste(pos) && ExistePecaOponente(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (ObterTabuleiro().PosicaoExiste(pos) && !ObterTabuleiro().ExistePeca(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                pos.DefinirValores(pos.Linha + 1, pos.Coluna);
            }
            if (ObterTabuleiro().PosicaoExiste(pos) && ExistePecaOponente(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (ObterTabuleiro().PosicaoExiste(pos) && !ObterTabuleiro().ExistePeca(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                pos.DefinirValores(pos.Linha, pos.Coluna - 1);
            }
            if (ObterTabuleiro().PosicaoExiste(pos) && ExistePecaOponente(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (ObterTabuleiro().PosicaoExiste(pos) && !ObterTabuleiro().ExistePeca(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                pos.DefinirValores(pos.Linha, pos.Coluna + 1);
            }
            if (ObterTabuleiro().PosicaoExiste(pos) && ExistePecaOponente(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Noroeste (NO)
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            while (ObterTabuleiro().PosicaoExiste(pos) && !ObterTabuleiro().ExistePeca(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                pos.DefinirValores(pos.Linha - 1, pos.Coluna - 1);
            }
            if (ObterTabuleiro().PosicaoExiste(pos) && ExistePecaOponente(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Nordeste (NE)
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (ObterTabuleiro().PosicaoExiste(pos) && !ObterTabuleiro().ExistePeca(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                pos.DefinirValores(pos.Linha - 1, pos.Coluna + 1);
            }
            if (ObterTabuleiro().PosicaoExiste(pos) && ExistePecaOponente(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Sudeste (SE)
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            while (ObterTabuleiro().PosicaoExiste(pos) && !ObterTabuleiro().ExistePeca(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                pos.DefinirValores(pos.Linha + 1, pos.Coluna + 1);
            }
            if (ObterTabuleiro().PosicaoExiste(pos) && ExistePecaOponente(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Sudoeste (SO)
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            while (ObterTabuleiro().PosicaoExiste(pos) && !ObterTabuleiro().ExistePeca(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                pos.DefinirValores(pos.Linha + 1, pos.Coluna - 1);
            }
            if (ObterTabuleiro().PosicaoExiste(pos) && ExistePecaOponente(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            return mat;
        }
    }
}
