using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class TocadoArgs
    {
        string nombre;
        Coordenada cord;

        public TocadoArgs(string _nombre, Coordenada _cord)
        {
            nombre = _nombre;
            cord = _cord;
        }
    }

    public class HundidoArgs
    {
        string nombre;

        public HundidoArgs(string _nombre)
        {
            nombre= _nombre;
        }
    }
}
