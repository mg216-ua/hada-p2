using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Coordenada
    {
        public int Fila
        {
            get
            {
                return Fila;
            }

            private set
            {
                if (value >= 0 && value <= 9)
                {
                    Fila = value;
                }
            }
        }

        public int Columna
        {
            get
            {
                return Columna;
            }

            private set
            {
                if (value >= 0 && value <= 9)
                {
                    Columna = value;
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
            this.Fila = int.Parse(f);
            this.Columna = int.Parse(c);
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

        public override int GetHashCode() => this.Fila.GetHashCode() ^ this.Columna.GetHashCode();

        public override bool Equals(object obj)
        {
            if (!(obj is Coordenada))
            {
                return false;
            }
            else
            {
                Coordenada c = obj as Coordenada;

                return this.Fila == c.Fila && this.Columna == c.Columna;
            }
        }

        public bool Equals(Coordenada c)
        {
            return this.Fila == c.Fila && this.Columna == c.Columna;
        }
    }
}
