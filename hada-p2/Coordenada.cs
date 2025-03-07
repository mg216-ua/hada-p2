using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Coordenada
    {
        // Homónimos privados de la Fila y Columna necesarios para poder implemntar la siguiente manera de invoación del get y set
        //      Simplificando, usamos el Fila y Columna como interfazes de acceso a propiedades privadas
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

        // Constructor por defecto
        public Coordenada()
        {
            this.Fila = 0;
            this.Columna = 0;
        }

        /// <summary>
        /// Constructor de la coordenada (int)
        /// </summary>
        /// <param name="_Fila">Fila</param>
        /// <param name="_Columna">Columna</param>
        public Coordenada(int _Fila, int _Columna)
        {
            this.Fila = _Fila;
            this.Columna = _Columna;
        }

        /// <summary>
        /// Constructor de la coordenada (string)
        /// </summary>
        /// <param name="_Fila">Fila</param>
        /// <param name="_Columna">Columna</param>
        public Coordenada(string _Fila, string _Columna)
        {
            this.Fila = Convert.ToInt32(_Fila);
            this.Columna = Convert.ToInt32(_Columna);
        }

        // Constructor de copia
        public Coordenada(Coordenada other)
        {
            this.Fila = other.Fila;
            this.Columna = other.Columna;
        }

        // To string asociado
        public override string ToString()
        {
            return "(" + this.Fila + "," + this.Columna + ")";
        }

        // hashcode asociado
        public override int GetHashCode()
        {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }

        // Equals asociado (Object)
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Coordenada)) return false;
            return Equals((Coordenada)obj); // Invocamos equals de Coordenada
        }

        // Equals asociado (Coordenada)
        public bool Equals(Coordenada other)
        {
            if (this.Columna == other.Columna && this.Fila == other.Fila) return true;
            else return false;
        }
    }
}

