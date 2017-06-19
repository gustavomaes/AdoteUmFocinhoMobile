using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoteUmFocinhoMobile.Custom
{
    public interface IAjusteImagem
    {
        byte[] Cortar(byte[] imagem, int largura, int altura);
    }
}
