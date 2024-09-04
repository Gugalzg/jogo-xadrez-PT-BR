using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_jogo.tabuleiroJogo;

namespace xadrez_jogo.xadrez.pecas
{
    public class Peao : PecaXadrez
    {
        private PartidaXadrez partidaXadrez;

        public Peao(Tabuleiro tabuleiro, Cor cor, PartidaXadrez partidaXadrez) : base(tabuleiro, cor)
        {
            this.partidaXadrez = partidaXadrez;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[ObterTabuleiro().Linhas(), ObterTabuleiro().Colunas()];
            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branco)
            {
                // Move um passo para frente
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (ObterTabuleiro().PosicaoExiste(pos) && !ObterTabuleiro().ExistePeca(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // Move dois passos para frente
                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                Posicao p2 = new Posicao(Posicao.Linha - 1, Posicao.Coluna);
                if (ObterTabuleiro().PosicaoExiste(pos) && !ObterTabuleiro().ExistePeca(pos) &&
                    ObterTabuleiro().PosicaoExiste(p2) && !ObterTabuleiro().ExistePeca(p2) &&
                    ContagemMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // Captura na diagonal esquerda
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (ObterTabuleiro().PosicaoExiste(pos) && ExistePecaOponente(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // Captura na diagonal direita
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (ObterTabuleiro().PosicaoExiste(pos) && ExistePecaOponente(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // Movimento especial: en passant branco
                if (Posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (ObterTabuleiro().PosicaoExiste(esquerda) && ExistePecaOponente(esquerda) &&
                        ObterTabuleiro().Peca(esquerda) == partidaXadrez.EnPassantVulneravel)
                    {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (ObterTabuleiro().PosicaoExiste(direita) && ExistePecaOponente(direita) &&
                        ObterTabuleiro().Peca(direita) == partidaXadrez.EnPassantVulneravel)
                    {
                        mat[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }
            else
            {
                // Move um passo para frente
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (ObterTabuleiro().PosicaoExiste(pos) && !ObterTabuleiro().ExistePeca(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // Move dois passos para frente
                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                Posicao p2 = new Posicao(Posicao.Linha + 1, Posicao.Coluna);
                if (ObterTabuleiro().PosicaoExiste(pos) && !ObterTabuleiro().ExistePeca(pos) &&
                    ObterTabuleiro().PosicaoExiste(p2) && !ObterTabuleiro().ExistePeca(p2) &&
                    ContagemMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // Captura na diagonal esquerda
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (ObterTabuleiro().PosicaoExiste(pos) && ExistePecaOponente(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // Captura na diagonal direita
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (ObterTabuleiro().PosicaoExiste(pos) && ExistePecaOponente(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // Movimento especial: en passant preto
                if (Posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (ObterTabuleiro().PosicaoExiste(esquerda) && ExistePecaOponente(esquerda) &&
                        ObterTabuleiro().Peca(esquerda) == partidaXadrez.EnPassantVulneravel)
                    {
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (ObterTabuleiro().PosicaoExiste(direita) && ExistePecaOponente(direita) &&
                        ObterTabuleiro().Peca(direita) == partidaXadrez.EnPassantVulneravel)
                    {
                        mat[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
