using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hada // HADA CON HACHE MAYUSCULA COMO H EN "HACHE MAYUSCULA"
{
    internal class Barco
    {
        // Parámetros de barcos
        public Dictionary<Coordenada, String> CoordenadasBarco {  get; private set; }
        public string Nombre { get; private set; }
        public int NumDanyos { get; private set; }
        public static int tablero { get; set; }

        // Eventos de barco
        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;

        /// <summary>
        /// Constructor de barcos
        /// </summary>
        /// <param name="_Nombre">Nombre</param>
        /// <param name="longitud">Longitud</param>
        /// <param name="orientacion">Orientación(h/v)</param>
        /// <param name="coordenadaInicio">Coordenada</param>
        /// <exception cref="ArgumentException"></exception>
        public Barco(string _Nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            // Declaramos diccionario vacío
            this.CoordenadasBarco = new Dictionary<Coordenada, String>();

            // Si la orientación es vertical:
            if (orientacion=='v')
            {
                // Si la coordenada final de barco está fuera del tablero --> error
                // La razon por la cual se usa "tablero" en vez de un valor fijo 9, es para poder permitir comprobación dentro del barco, incluso si los parámetros
                //      pasados alconstructor siempre serán verificados previamente.
                if ((coordenadaInicio.Fila + longitud) > tablero) { throw new ArgumentException("Imposible position and length combination"); }
                // Si el barco cabe en el tablero global:
                else{ for (int i = 0; i < longitud; i++)
                    {
                        // Añadimos a sus coordenadas, los puntos que ocupa (Ya que es vertical, en fila de - a +)
                        this.CoordenadasBarco.Add(new Coordenada((coordenadaInicio.Fila+i), coordenadaInicio.Columna), _Nombre);
                    } }
            } else if (orientacion=='h') // Si es horizontal: Hace lo mismo pero comprueba columnas
            {
                if ((coordenadaInicio.Columna + longitud) > tablero) { throw new ArgumentException("Imposible position and length combination"); }
                else { for (int i = 0;i<longitud;i++)
                     {
                        this.CoordenadasBarco.Add(new Coordenada(coordenadaInicio.Fila, (coordenadaInicio.Columna + i)), _Nombre);
                     } }
            }
            else // En el resto de casos:
            {
                // EL intento se ignora y se devuelve una excepción
                throw new ArgumentException("Wrong orientation");
            }

            if (!(_Nombre is null))
            {
                this.Nombre = _Nombre;
            } else this.Nombre = "VOID"; // "VOID": Nombre por defecto dado albarco en caso de que el nombre sea nulo
            
            this.NumDanyos = 0;
        }

        // Boolenao de verificación de hundimiento
        public bool hundido()
        {
            // Simplemente comprobamos si el barco contiene alguna coordenada que no sea tocada "$_T", es decir,
            //      que el nombre asociado a la coordenada, sea igual al nombre original
            if (!this.CoordenadasBarco.ContainsValue(this.Nombre))
            {
                return true;
            } else { return false; }
        }

        // Booleano de verificación de disparo
        public void Disparo(Coordenada cord)
        {
            // Si la coordenada forma parte del varco y no ha sido disparada ya
            if (this.CoordenadasBarco.ContainsKey(cord) && !this.CoordenadasBarco[cord].EndsWith("_T"))
            {
                this.CoordenadasBarco[cord] += "_T"; // Añadimos _T al nombre asoiado
                eventoTocado.Invoke(this, new TocadoArgs(this.Nombre, cord)); // Invocamos evento de toque
                this.NumDanyos++; // Incrementamos los daños
                // Comprobamos que el barco esté hundido, es caso de que lo sea, invocamos evento de hundición
                if (this.hundido()) { eventoHundido.Invoke(this, new HundidoArgs(this.Nombre)); }
            } else { return; } // Si la coordenada no da, la ignoramos
        }

        // Función que imprime la lista de coordenadas asociadas al barco
        public string PrintCoordenates()
        {
            string str=""; // String base para construir la cadena final
            foreach (Coordenada cord in this.CoordenadasBarco.Keys) { // Para cada coordenada en barco, la añadimos a la cadena
                str +=" [" + cord.ToString()+" :" + this.CoordenadasBarco[cord] + "]";
            }
            return str;
        }

        // To string asoiado al barco
        public override string ToString()
        {
            return("["+this.Nombre+"] - DAÑOS: ["+this.NumDanyos+"] - HUNDIDO: ["+this.hundido()+"] - COORDENADAS:"+this.PrintCoordenates());
        }
    }
}
