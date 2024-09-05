using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_jogo.xadrez;

namespace xadrez_jogo.tabuleiroJogo
{
    public class Tabuleiro
    {
        public int Id { get; set; }
        private int linhas;
        private int colunas;
        
        public ICollection<PecaXadrez> PecasXadrez { get; set; } = new List<PecaXadrez>();
        private Peca[,] pecas;

        public Tabuleiro() {
       
        }
        public Tabuleiro(int linhas, int colunas)
        {
            if (linhas < 1 || colunas < 1)
            {
                throw new TabuleiroException("Erro ao criar tabuleiro: deve haver pelo menos 1 linha e 1 coluna.");
            }
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];
        }

        public int Linhas()
        {
             return linhas;       
        }

        public int Colunas()
        {
            return colunas; 
        }

        public Peca Peca(int linha, int coluna)
        {
            if (!PosicaoExiste(linha, coluna))
            {
                throw new TabuleiroException("Posição fora do tabuleiro.");
            }
            return pecas[linha, coluna];
        }

        public Peca Peca(Posicao posicao)
        {
            if (!PosicaoExiste(posicao))
            {
                throw new TabuleiroException("Posição fora do tabuleiro.");
            }
            return pecas[posicao.Linha, posicao.Coluna];
        }

        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if (ExistePeca(posicao))
            {
                throw new TabuleiroException("Já existe uma peça na posição " + posicao);
            }
            pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        public Peca RemoverPeca(Posicao posicao)
        {
            if (!PosicaoExiste(posicao))
            {
                throw new TabuleiroException("Posição fora do tabuleiro.");
            }
            if (Peca(posicao) == null)
            {
                return null;
            }
            Peca aux = Peca(posicao);
            aux.Posicao = null;
            pecas[posicao.Linha, posicao.Coluna] = null;
            return aux;
        }

        public bool PosicaoExiste(int linha, int coluna)
        {
            return linha >= 0 && linha < linhas && coluna >= 0 && coluna < colunas;
        }

        public bool PosicaoExiste(Posicao posicao)
        {
            return PosicaoExiste(posicao.Linha, posicao.Coluna);
        }

        public bool ExistePeca(Posicao posicao)
        {
            if (!PosicaoExiste(posicao))
            {
                throw new TabuleiroException("Posição fora do tabuleiro.");
            }
            return Peca(posicao) != null;
        }
    }
}

