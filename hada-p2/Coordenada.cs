using System;
namespace Hada
{
    public class Coordenada
    {
        public int Fila
        {
            get
            {return (int)this.Fila;}
            set
            {if (value >= 0 && value <= 9) { this.Fila = value; }}
        }
        public int Columna
        {
            get
            {return (int)this.Fila;}
            set
            {if (value >= 0 && value <= 9) { this.Fila = value; }}
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

        public string ToString()
        {
            return Console.WriteLine("("+this.Fila +","+ this.Columna+")");
        }

        public  int GetHashCode()
        {
            return this.Fila.GetHashCode()^this.Columna.getHashCode();
        }

        public bool Equals(object obj) 
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
