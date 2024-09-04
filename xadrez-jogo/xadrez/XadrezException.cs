using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_jogo.tabuleiroJogo;

namespace xadrez_jogo.xadrez
{
    public class XadrezException : TabuleiroException
    {
        public XadrezException(string mensagem) : base(mensagem)
        {
        }

    }
}
