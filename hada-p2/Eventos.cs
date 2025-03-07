using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class TocadoArgs
    {
        //Definimos los variables en la manera de privato
        private string _nombre;
        private Coordenada _coordenadaImpacto;

        //Definimos los variables
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

        //Constructor de evento Tocado
        public TocadoArgs(string nombre, Coordenada coordenada)
        {
            this.Nombre = nombre;
            this.CoordenadaImpacto = coordenada;
        }
    }

    internal class HundidoArgs
    {
        //Variable de nombre en manera privado
        private string _nombre;

        //Definimos el nombre de barco como variable
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        //Constructor de evento Hundido
        public HundidoArgs(string nombre)
        {
            this.Nombre = nombre;
        }
    }
}
