using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_jogo.tabuleiroJogo;
using xadrez_jogo.xadrez.pecas;

namespace xadrez_jogo.xadrez
{
    public class PartidaXadrez
    {

        private int turno;
        private Cor jogadorAtual;
        private Tabuleiro tabuleiro;
        private bool xeque;
        private bool xequeMate;
        private PecaXadrez enPassantVulneravel;
        private PecaXadrez promovida;

        private List<Peca> pecasNoTabuleiro = new List<Peca>();
        private List<Peca> pecasCapturadas = new List<Peca>();

        public PartidaXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            InicialSetup();
        }

        public int Turno => turno;
        public Cor JogadorAtual => jogadorAtual;
        public bool Xeque => xeque;
        public bool XequeMate => xequeMate;
        public PecaXadrez EnPassantVulneravel => enPassantVulneravel;
        public PecaXadrez Promovida => promovida;

        public PecaXadrez[,] Pecas
        {
            get
            {
                PecaXadrez[,] matriz = new PecaXadrez[tabuleiro.Linhas(), tabuleiro.Colunas()];
                for (int i = 0; i < tabuleiro.Linhas(); i++)
                {
                    for (int j = 0; j < tabuleiro.Colunas(); j++)
                    {
                        matriz[i, j] = (PecaXadrez)tabuleiro.Peca(i, j);
                    }
                }
                return matriz;
            }
        }

        public bool[,] MovimentosPossiveis(PosicaoXadrez origem)
        {
            Posicao posicao = origem.ParaPosicao();
            ValidarPosicaoDeOrigem(posicao);
            return tabuleiro.Peca(posicao).MovimentosPossiveis();
        }

        public PecaXadrez ExecutarMovimentoXadrez(PosicaoXadrez origem, PosicaoXadrez destino)
        {
            Posicao posicaoOrigem = origem.ParaPosicao();
            Posicao posicaoDestino = destino.ParaPosicao();
            ValidarPosicaoDeOrigem(posicaoOrigem);
            ValidarPosicaoDeDestino(posicaoOrigem, posicaoDestino);
            Peca pecaCapturada = RealizarMovimento(posicaoOrigem, posicaoDestino);

            if (TestarXeque(jogadorAtual))
            {
                DesfazerMovimento(posicaoOrigem, posicaoDestino, pecaCapturada);
                throw new XadrezException("Você não pode se colocar em xeque.");
            }

            PecaXadrez pecaMovida = (PecaXadrez)tabuleiro.Peca(posicaoDestino);

            // Promoção especial
            promovida = null;
            if (pecaMovida is Peao)
            {
                if ((pecaMovida.Cor == Cor.Branco && posicaoDestino.Linha == 0) || (pecaMovida.Cor == Cor.Preto && posicaoDestino.Linha == 7))
                {
                    promovida = (PecaXadrez)tabuleiro.Peca(posicaoDestino);
                    promovida = SubstituirPecaPromovida("D");
                }
            }

            xeque = TestarXeque(Adversaria(jogadorAtual));

            if (TestarXequeMate(Adversaria(jogadorAtual)))
            {
                xequeMate = true;
            }
            else
            {
                ProximoTurno();
            }

            // Jogada especial En Passant
            if (pecaMovida is Peao && (posicaoDestino.Linha == posicaoOrigem.Linha - 2 || posicaoDestino.Linha == posicaoOrigem.Linha + 2))
            {
                enPassantVulneravel = pecaMovida;
            }
            else
            {
                enPassantVulneravel = null;
            }

            return (PecaXadrez)pecaCapturada;
        }

        public PecaXadrez SubstituirPecaPromovida(string tipo)
        {
            if (promovida == null)
            {
                throw new InvalidOperationException("Não há peça a ser promovida.");
            }
            if (tipo != "B" && tipo != "C" && tipo != "T" && tipo != "D")
            {
                return promovida;
            }
            Posicao pos = promovida.ObterPosicaoXadrez().ParaPosicao();
            Peca p = tabuleiro.RemoverPeca(pos);
            pecasNoTabuleiro.Remove(p);
            PecaXadrez novaPeca = NovaPeca(tipo, promovida.Cor);
            tabuleiro.ColocarPeca(novaPeca, pos);
            pecasNoTabuleiro.Add(novaPeca);

            return novaPeca;
        }

        private PecaXadrez NovaPeca(string tipo, Cor cor)
        {
            if (tipo == "B") return new Bispo(tabuleiro, cor);
            if (tipo == "C") return new Cavalo(tabuleiro, cor);
            if (tipo == "Q") return new Rainha(tabuleiro, cor);
            return new Torre(tabuleiro, cor);
        }

        private Peca RealizarMovimento(Posicao origem, Posicao destino)
        {
            PecaXadrez p = (PecaXadrez)tabuleiro.RemoverPeca(origem);
            p.IncrementarContagemMovimentos();
            Peca pecaCapturada = tabuleiro.RemoverPeca(destino);
            tabuleiro.ColocarPeca(p, destino);

            if (pecaCapturada != null)
            {
                pecasNoTabuleiro.Remove(pecaCapturada);
                pecasCapturadas.Add(pecaCapturada);
            }

            // Jogada especial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                PecaXadrez torre = (PecaXadrez)tabuleiro.RemoverPeca(origemT);
                tabuleiro.ColocarPeca(torre, destinoT);
                torre.IncrementarContagemMovimentos();
            }

            // Jogada especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                PecaXadrez torre = (PecaXadrez)tabuleiro.RemoverPeca(origemT);
                tabuleiro.ColocarPeca(torre, destinoT);
                torre.IncrementarContagemMovimentos();
            }

            // Jogada especial En Passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posPeao;
                    if (p.Cor == Cor.Branco)
                    {
                        posPeao = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posPeao = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = tabuleiro.RemoverPeca(posPeao);
                    pecasCapturadas.Add(pecaCapturada);
                    pecasNoTabuleiro.Remove(pecaCapturada);
                }
            }
            return pecaCapturada;
        }

        private void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            PecaXadrez p = (PecaXadrez)tabuleiro.RemoverPeca(destino);
            p.DecrementarContagemMovimentos();
            tabuleiro.ColocarPeca(p, origem);

            if (pecaCapturada != null)
            {
                tabuleiro.ColocarPeca(pecaCapturada, destino);
                pecasCapturadas.Remove(pecaCapturada);
                pecasNoTabuleiro.Add(pecaCapturada);
            }

            // Jogada especial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                PecaXadrez torre = (PecaXadrez)tabuleiro.RemoverPeca(destinoT);
                tabuleiro.ColocarPeca(torre, origemT);
                torre.DecrementarContagemMovimentos();
            }

            // Jogada especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                PecaXadrez torre = (PecaXadrez)tabuleiro.RemoverPeca(destinoT);
                tabuleiro.ColocarPeca(torre, origemT);
                torre.DecrementarContagemMovimentos();
            }

            // Jogada especial En Passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == enPassantVulneravel)
                {
                    PecaXadrez peao = (PecaXadrez)tabuleiro.RemoverPeca(destino);
                    Posicao posPeao;
                    if (p.Cor == Cor.Branco)
                    {
                        posPeao = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posPeao = new Posicao(4, destino.Coluna);
                    }
                    tabuleiro.ColocarPeca(peao, posPeao);
                }
            }
        }

        private void ValidarPosicaoDeOrigem(Posicao posicao)
        {
            if (!tabuleiro.ExistePeca(posicao))
            {
                throw new XadrezException("Não há peça na posição de origem.");
            }
            if (jogadorAtual != ((PecaXadrez)tabuleiro.Peca(posicao)).Cor)
            {
                throw new XadrezException("A peça escolhida não é sua.");
            }

            if (!tabuleiro.Peca(posicao).ExisteAlgumMovimentoPossivel())
            {
                throw new XadrezException("Não há movimentos possíveis para a peça escolhida.");
            }
        }

        private void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tabuleiro.Peca(origem).MovimentoPossivel(destino))
            {
                throw new XadrezException("A peça escolhida não pode se mover para a posição de destino.");
            }
        }

        private void ProximoTurno()
        {
            turno++;
            jogadorAtual = (jogadorAtual == Cor.Branco) ? Cor.Preto : Cor.Branco;
        }

        private Cor Adversaria(Cor cor)
        {
            return (cor == Cor.Branco) ? Cor.Preto : Cor.Branco;
        }

        private PecaXadrez Rei(Cor cor)
        {
            return pecasNoTabuleiro.OfType<PecaXadrez>().FirstOrDefault(x => x.Cor == cor && x is Rei)
                   ?? throw new InvalidOperationException($"Não há rei {cor} no tabuleiro.");
        }

       private bool TestarXeque(Cor cor)
        {
            Posicao posicaoRei = Rei(cor).ObterPosicaoXadrez().ParaPosicao();
        var pecasAdversarias = pecasNoTabuleiro
            .Where(x => ((PecaXadrez)x).Cor == Adversaria(cor))
            .ToList();
        foreach (var p in pecasAdversarias)
        {
            bool[,] mat = p.MovimentosPossiveis();
            if (mat[posicaoRei.Linha, posicaoRei.Coluna])
            {
                return true;
            }
        }
        return false;
    }

        private bool TestarXequeMate(Cor cor)
        {
            if (!TestarXeque(cor))
            {
                return false;
            }

            foreach (PecaXadrez peca in pecasNoTabuleiro.OfType<PecaXadrez>().Where(x => x.Cor == cor))
            {
                bool[,] movimentos = peca.MovimentosPossiveis();
                for (int i = 0; i < tabuleiro.Linhas(); i++)
                {
                    for (int j = 0; j < tabuleiro.Colunas(); j++)
                    {
                        if (movimentos[i, j])
                        {
                            Posicao origem = peca.ObterPosicaoXadrez().ParaPosicao();
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = RealizarMovimento(origem, destino);
                            bool testarXeque = TestarXeque(cor);
                            DesfazerMovimento(origem, destino, pecaCapturada);
                            if (!testarXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private void ColocarNovaPeca(char coluna, int linha, PecaXadrez peca)
        {
            tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ParaPosicao());
            pecasNoTabuleiro.Add(peca);
        }

        private void InicialSetup()
        {
            ColocarNovaPeca('a', 1, new Torre(tabuleiro, Cor.Branco));
            ColocarNovaPeca('b', 1, new Cavalo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('c', 1, new Bispo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('d', 1, new Rainha(tabuleiro, Cor.Branco));
            ColocarNovaPeca('e', 1, new Rei(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('f', 1, new Bispo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('g', 1, new Cavalo(tabuleiro, Cor.Branco));
            ColocarNovaPeca('h', 1, new Torre(tabuleiro, Cor.Branco));
            ColocarNovaPeca('a', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('b', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('c', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('d', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('e', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('f', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('g', 2, new Peao(tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('h', 2, new Peao(tabuleiro, Cor.Branco, this));

            ColocarNovaPeca('a', 8, new Torre(tabuleiro, Cor.Preto));
            ColocarNovaPeca('b', 8, new Cavalo(tabuleiro, Cor.Preto));
            ColocarNovaPeca('c', 8, new Bispo(tabuleiro, Cor.Preto));
            ColocarNovaPeca('d', 8, new Rainha(tabuleiro, Cor.Preto));
            ColocarNovaPeca('e', 8, new Rei(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('f', 8, new Bispo(tabuleiro, Cor.Preto));
            ColocarNovaPeca('g', 8, new Cavalo(tabuleiro, Cor.Preto));
            ColocarNovaPeca('h', 8, new Torre(tabuleiro, Cor.Preto));
            ColocarNovaPeca('a', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('b', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('c', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('d', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('e', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('f', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('g', 7, new Peao(tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('h', 7, new Peao(tabuleiro, Cor.Preto, this));
        }
    }

}

