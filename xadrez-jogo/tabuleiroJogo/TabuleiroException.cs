using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xadrez_jogo.tabuleiroJogo
{
   
    [Serializable]
    public class TabuleiroException : Exception
    {
        
        private const long SerialVersionUid = 1L;
        
        public TabuleiroException(string mensagem) : base(mensagem)
        {
        }

    protected TabuleiroException(System.Runtime.Serialization.SerializationInfo info, 
    System.Runtime.Serialization.StreamingContext context) : base (info, context)
    {
    }

    }
}
