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
        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;
        public Dictionary<Coordenada, String> CoordenadasBarco
        {
            get;
        }

        private string _nombre;

        public string Nombre 
        {
            get
            {
                return _nombre;
            }

            set
            {
                _nombre = value;
            }
        }

        public int NumDanyos
        {
            get;

            private set;
        }

        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            this.Nombre = nombre;
            this.NumDanyos = 0;
            CoordenadasBarco = new Dictionary<Coordenada, String>();

            for (int i = 0; i < longitud; i++)
            {
                int row = coordenadaInicio.Fila + (orientacion == 'v' ? i : 0);
                int col = coordenadaInicio.Columna + (orientacion == 'h' ? i : 0);
                Coordenada nueva = new Coordenada(row, col);
                CoordenadasBarco[nueva] = Nombre;
            }
        }

        public void Disparo(Coordenada c)
        {
            if (CoordenadasBarco.ContainsKey(c) &&
                !CoordenadasBarco[c].EndsWith("_T"))
            {
                CoordenadasBarco[c] += "_T";

                NumDanyos++;
                eventoTocado?.Invoke(this, new TocadoArgs(this.Nombre, c));

                if (this.hundido())
                {
                    eventoHundido?.Invoke(this, new HundidoArgs(this.Nombre));
                }
            }
        }

        public bool hundido()
        {
            foreach (var etiqueta in CoordenadasBarco.Values)
            {
                if (etiqueta == this.Nombre)
                {
                    return false;
                }
            }
            return true;
        }

        public string ToString()
        {
            string texto = "";

            texto = texto + "[" + this.Nombre + "] - DAÑOS: [" + this.NumDanyos + "] - HUNDIDO: [" + this.hundido() + "] - ";
            texto = texto + "COORDENADAS: ";

            foreach (var elemento in CoordenadasBarco)
            {
                texto = texto + "[(" + elemento.Key.Fila + "," + elemento.Key.Columna + ") :" + elemento.Value + "] ";
            }

            return texto;
        }
    }
}
