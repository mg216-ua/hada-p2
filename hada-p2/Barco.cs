using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Barco
    {
        public Dictionary<Coordenada, string> CoordenadasBarco
        {
            get; private set;
        }

        public string Nombre 
        {
            get; private set;
        }

        public int NumDanyos
        {
            get; private set;
        }

        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            this.Nombre = nombre;
            this.CoordenadasBarco = new Dictionary<Coordenada, string>();
            this.NumDanyos = 0;

            for (int i = 0; i < longitud; i++)
            {
                
            }
        }

        public void Disparo(Coordenada c)
        {

        }

        public bool hundido()
        {
            return true;
        }

        public string ToString()
        {
            string text = "";

            text = text + "[" + this.Nombre + "]";
            text = text + " - DAÑOS: [" + this.NumDanyos + "] - ";
            text = text + "HUNDIDO: " + this.hundido() + "] - ";
            text = text + "COORDENADAS:";

            for (int i = 0; i < i; i++)
            {
                
            }


            return text;
        }

        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;
    }
}
