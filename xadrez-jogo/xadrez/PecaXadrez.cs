using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_jogo.tabuleiroJogo;

namespace xadrez_jogo.xadrez
{
    public abstract class PecaXadrez : Peca
    {
        public int Id { get; set; }
        public Cor Cor { get; private set; }
        public int ContagemMovimentos { get; private set; }

        public int TabuleiroId { get; set; }

        // Construtor padrão necessário para o Entity Framework
        protected PecaXadrez() { }

        // Construtor com parâmetros para lógica do jogo
        protected PecaXadrez(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro)
        {
            Cor = cor;
        }

        public void IncrementarContagemMovimentos()
        {
            ContagemMovimentos++;
        }

        public void DecrementarContagemMovimentos()
        {
            ContagemMovimentos--;
        }

        public PosicaoXadrez ObterPosicaoXadrez()
        {
            return PosicaoXadrez.DePosicao(Posicao);
        }

        protected bool ExistePecaOponente(Posicao pos)
        {
            PecaXadrez p = ObterTabuleiro().Peca(pos) as PecaXadrez;
            return p != null && p.Cor != this.Cor;
        }
    }
}

