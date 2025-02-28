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
        public Dictionary<Coordenada, String> CoordenadasBarco {  get; private set; }
        public string Nombre { get; private set; }
        public int NumDanyos { get; private set; }

        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;

        public Barco(string _Nombre, int longitud, char orienacion, Coordenada coordenadaInicio)
        {
            if (orienacion=='h')
            {
                if ((coordenadaInicio.Fila + longitud) > 9) { throw new ArgumentException("Imposible position and length combination"); }
                else{ for (int i = 0; i < longitud; i++)
                    {
                        this.CoordenadasBarco.Add(new Coordenada((coordenadaInicio.Fila+i), coordenadaInicio.Columna), _Nombre);
                    } }
            } else if (orienacion=='v')
            {
                if ((coordenadaInicio.Columna + longitud) > 9) { throw new ArgumentException("Imposible position and length combination"); }
                else { for (int i = 0;i<longitud;i++)
                     {
                        this.CoordenadasBarco.Add(new Coordenada(coordenadaInicio.Fila, (coordenadaInicio.Columna + i)), _Nombre);
                     } }
            }
            else
            {
                throw new ArgumentException("Wrong orientation");
            }
            
            // Check if name is null?
            this.Nombre = _Nombre;
            this.NumDanyos = 0;
        }

        public bool hundido()
        {
            if (!this.CoordenadasBarco.ContainsValue(this.Nombre))
            {
                return true;
            } else { return false; }
        }

        public void Disparo(Coordenada cord)
        {
            if (this.CoordenadasBarco.ContainsKey(cord) && !this.CoordenadasBarco[cord].EndsWith("_T"))
            {
                this.CoordenadasBarco[cord] += "_T";
                eventoTocado.Invoke(this, new TocadoArgs(this.Nombre, cord));
                this.NumDanyos++;   
                if (this.hundido()) { eventoHundido.Invoke(this, new HundidoArgs(this.Nombre)); }
            } else { return; }
        }

        public string PrintCoordenates()
        {
            string str="";
            foreach (Coordenada cord in this.CoordenadasBarco.Keys) {
                str +=" [" + cord.ToString()+" :" + this.CoordenadasBarco[cord] + "]";
            }
            return str;
        }

        public override string ToString()
        {
            return("["+this.Nombre+"] - DAÑOS: ["+this.NumDanyos+"] - HUNDIDO: ["+this.hundido()+"] - COORDENADAS:"+this.PrintCoordenates());
        }
    }
}
