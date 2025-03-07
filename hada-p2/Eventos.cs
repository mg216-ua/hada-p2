using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class TocadoArgs : EventArgs // Clase tocado Args
    {
        public string nombre { get; }
        public Coordenada cord { get; }

        public TocadoArgs(string _nombre, Coordenada _cord)
        {
            nombre = _nombre;
            cord = _cord;
        }
    }

    public class HundidoArgs : EventArgs // Clase Hundido Args
    {
        public string nombre{ get; }

        public HundidoArgs(string _nombre)
        {
            nombre= _nombre;
        }
    }
}
