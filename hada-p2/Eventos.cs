using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class TocadoArgs
    {
        private string _nombre;
        private Coordenada _coordenadaImpacto;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public Coordenada CoordenadaImpacto
        {
            get { return _coordenadaImpacto; }
            set { _coordenadaImpacto = value; }
        }

        public TocadoArgs(string nombre, Coordenada coordenada)
        {
            this.Nombre = nombre;
            this.CoordenadaImpacto = coordenada;
        }
    }

    internal class HundidoArgs
    {
        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public HundidoArgs(string nombre)
        {
            this.Nombre = nombre;
        }
    }
}
