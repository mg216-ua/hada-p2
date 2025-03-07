using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Coordenada
    {
        //Definimos nuestros variables en la manera de privatao
        private int _fila;
        private int _columna;

        public int Fila
        {
            get
            {
                return _fila;
            }

            private set
            {
                if (value >= 0 && value <= 9)
                {
                    _fila = value;
                }
            }
        }

        public int Columna
        {
            get
            {
                return _columna;
            }

            private set
            {
                if (value >= 0 && value <= 9)
                {
                    _columna = value;
                }
            }
        }

        //Constructor de coordenada
        public Coordenada()
        {
            this.Fila = 0;
            this.Columna = 0;
        }

        //Constructor de coordenada segun los valores numericos de fila y columna
        public Coordenada(int f, int c)
        {
            this.Fila = f;
            this.Columna = c;
        }

        //Constructor de coordenada, si los valores definidos serían en manera de cadena
        public Coordenada(string f, string c)
        {
            this.Fila = Convert.ToInt32(f);
            this.Columna = Convert.ToInt32(c);
        }

        //Constructor de copia
        public Coordenada(Coordenada other)
        {
            this.Fila = other.Fila;
            this.Columna = other.Columna;
        }

        //ToString de demonstrar la coodenada de barco
        public override string ToString()
        {
            return "(" + this.Fila + "," + this.Columna + ")";
        }

        //Obtener el entero de HashCode
        public override int GetHashCode()
        {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }

        //Booleano de equals si el objeto = coordena this
        public override bool Equals(object obj)
        {
            //Si objeto es nullo ==> false
            if (obj == null)
            {
                return false;
            }

            //Si objeto contiene el mismo valor como this ==> true
            if (this == obj)
            {
                return true;
            }
            else
            {
                //En caso contrario convertimos objeto al coordenada y definimos si son mismos
                Coordenada aux = (Coordenada) obj;
                return this.Fila == aux.Fila && this.Columna == aux.Columna;
            }
        }

        //Booleano de definir si la coordenada dada y introducida son mismos
        public bool Equals(Coordenada c)
        {
            //Si la coordenada introducida es nullo ==> false
            if (c == null)
            {
                return false;
            }

            //Si la coordenada tiene mismos caracteristicas ==> true
            if (this == c)
            {
                return true;
            }
            else
            {
                //En caso contrario, comprobamos si los valores dados son mismos
                return this.Fila == c.Fila && this.Columna == c.Columna;
            }
        }
    }
}
