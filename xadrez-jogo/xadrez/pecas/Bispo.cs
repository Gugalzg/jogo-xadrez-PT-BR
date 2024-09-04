using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_jogo.tabuleiroJogo;

namespace xadrez_jogo.xadrez.pecas
{  
        public class Bispo : PecaXadrez
        {
            public Bispo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
            {
            }

            public override string ToString()
            {
                return "B";
            }

            public override bool[,] MovimentosPossiveis()
            {
                bool[,] mat = new bool[ObterTabuleiro().Linhas(), ObterTabuleiro().Colunas()];

                Posicao pos = new Posicao(0, 0);

                // Noroeste
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                while (ObterTabuleiro().PosicaoExiste(pos) && ! ObterTabuleiro().ExistePeca(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                    pos.DefinirValores(pos.Linha - 1, pos.Coluna - 1);
                }
                if (ObterTabuleiro().PosicaoExiste(pos) && ExistePecaOponente(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // Nordeste
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

                // Sudeste
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

                // Sudoeste
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

