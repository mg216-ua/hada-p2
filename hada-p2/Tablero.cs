using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Tablero
    {
        public int TamTablero
        {
            get { return TamTablero; }

            private set 
            {
                if (value <= 4 && value >= 9)
                {
                    
                }
            }
        }

        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        public Tablero(int tamTablero, List<Barco> barcos)
        {

        }
    }
}
