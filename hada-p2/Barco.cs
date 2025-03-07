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
        //Eventos definidos para futuro ejecutacion
        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;

        //Obtenemos los valores de dicionario de Coordenadas de Barcos
        public Dictionary<Coordenada, String> CoordenadasBarco
        {
            get;
        }

        //Valor private de string de definicion de variable de clase
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

        //Variable de numero daños
        public int NumDanyos
        {
            get;

            private set;
        }

        //Constructor de Barco segun su nombre, longitud, orientacion y coordenada iniciado
        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            //Definimos los valores necesarios
            this.Nombre = nombre;
            this.NumDanyos = 0;
            CoordenadasBarco = new Dictionary<Coordenada, String>();

            //ponemos los restos de caracteristicas y creamos barco
            for (int i = 0; i < longitud; i++)
            {
                int row = coordenadaInicio.Fila + (orientacion == 'v' ? i : 0);
                int col = coordenadaInicio.Columna + (orientacion == 'h' ? i : 0);
                Coordenada nueva = new Coordenada(row, col);
                CoordenadasBarco[nueva] = Nombre;
            }
        }

        //Funcion Disparo
        public void Disparo(Coordenada c)
        {
            //Si en las coordenadas donde hay barcos existe una clave con la coordenada
            //que buscamos y no este ya tocada
            if (CoordenadasBarco.ContainsKey(c) &&
                !CoordenadasBarco[c].EndsWith("_T"))
            {
                //Marcamos como tocado
                CoordenadasBarco[c] += "_T";
                //Incrementamos el numero de daños
                NumDanyos++;
                eventoTocado?.Invoke(this, new TocadoArgs(this.Nombre, c));

                if (this.hundido())
                {
                    eventoHundido?.Invoke(this, new HundidoArgs(this.Nombre));
                }
            }
        }

        //Booleano si el barco es hundido o no
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

        public override string ToString()
        {
            string texto = "";

            //Concatenamos los datos
            texto = texto + "[" + this.Nombre + "] - DAÑOS: [" + this.NumDanyos + "] - HUNDIDO: [" + this.hundido() + "] - ";
            texto = texto + "COORDENADAS: ";

            //Concatenamos las coordenadas
            foreach (var elemento in CoordenadasBarco)
            {
                texto = texto + "[(" + elemento.Key.Fila + "," + elemento.Key.Columna + ") :" + elemento.Value + "] ";
            }

            return texto;
        }
    }
}
