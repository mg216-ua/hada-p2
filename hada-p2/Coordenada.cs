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
            get => Fila;

            private set => Fila = (value >= 0 && value <= 9) ? value : Fila;
        }

        public int Columna
        {
            get => Columna;

            private set => Columna = (value >= 0 && value <= 9) ? value : Columna;
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

        public override bool Equals(object obj) => obj is Coordenada c && c.Fila == this.Fila && c.Columna == this.Columna;

        public bool Equals(Coordenada c) => c != null && c.Fila == this.Fila && c.Columna == this.Columna;
    }
}
