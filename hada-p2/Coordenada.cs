using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Coordenada
    {
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
                    _fila = value;
                }
            }
        }

        public Coordenada()
        {
            this.Fila = 0;
            this.Columna = 0;
        }

        public Coordenada(int f, int c)
        {
            this.Fila = f;
            this.Columna = c;
        }

        public Coordenada(string f, string c)
        {
            this.Fila = Convert.ToInt32(f);
            this.Columna = Convert.ToInt32(c);
        }

        public Coordenada(Coordenada other)
        {
            this.Fila = other.Fila;
            this.Columna = other.Columna;
        }

        public override string ToString()
        {
            return "(" + this.Fila + "," + this.Columna + ")";
        }

        public override int GetHashCode()
        {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (this == obj)
            {
                return true;
            }
            else
            {
                Coordenada aux = (Coordenada)obj;
                return this.Fila == aux.Fila && this.Columna == aux.Columna;
            }
        }

        public bool Equals(Coordenada c)
        {
            if (c == null)
            {
                return false;
            }

            if (this == c)
            {
                return true;
            }
            else
            {
                return this.Fila == c.Fila && this.Columna == c.Columna;
            }
        }
    }
}
