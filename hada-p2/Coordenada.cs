using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Coordenada
    {
        private int fila;
        private int columna;

        public int Fila
        {
            get
            { return this.fila; }
            private set
            { if (value >= 0 && value <= 9) { this.fila = value; } }
        }
        public int Columna
        {
            get
            { return this.columna; }
            private set
            { if (value >= 0 && value <= 9) { this.columna = value; } }
        }

        public Coordenada()
        {
            this.Fila = 0;
            this.Columna = 0;
        }

        public Coordenada(int _Fila, int _Columna)
        {
            this.Fila = _Fila;
            this.Columna = _Columna;
        }

        public Coordenada(string _Fila, string _Columna)
        {
            this.Fila = Convert.ToInt32(_Fila);
            this.Columna = Convert.ToInt32(_Columna);
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
            if (obj == null) return false;
            if (!(obj is Coordenada)) return false;
            return Equals((Coordenada)obj);
        }

        public bool Equals(Coordenada other)
        {
            if (this.Columna == other.Columna && this.Fila == other.Fila) return true;
            else return false;
        }
    }
}

