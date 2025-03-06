using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Tablero
    {
        private int _tamTablero;
        public int TamTablero
        {
            get { return _tamTablero; }
            set
            {
                if (value >= 4 && value <= 9)
                {
                    _tamTablero = value;
                }
            }
        }

        private List<Coordenada> CoordenadasDisparadas;
        private List<Coordenada> CoordenadasTocadas;
        private List<Barco> Barcos;
        private List<Barco> BarcosEliminados;
        private Dictionary<Coordenada, string> CasillasTablero;
        public event EventHandler<EventArgs> eventoFinPartida;

        public Tablero(int tamTablero, List<Barco> barcos)
        {
            this.TamTablero = tamTablero;
            this.Barcos = barcos;

            CoordenadasDisparadas = new List<Coordenada>();
            CoordenadasTocadas = new List<Coordenada>();
            BarcosEliminados = new List<Barco>();

            CasillasTablero = new Dictionary<Coordenada, string>();

            inicializarCasillasTablero();

            foreach (var barco in Barcos)
            {
                barco.eventoTocado += cuandoEventoTocado;
                barco.eventoHundido += cuandoEventoHundido;
            }

        }

        private void inicializarCasillasTablero()
        {
            CasillasTablero.Clear();

            for (int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    CasillasTablero[new Coordenada(i, j)] = "AGUA";
                }
            }

            foreach (var barco in this.Barcos)
            {
                foreach (var coordenada in barco.CoordenadasBarco)
                {
                    if (coordenada.Key.Fila >= 0 && coordenada.Key.Columna < TamTablero
                        && coordenada.Key.Columna >= 0 && coordenada.Key.Columna < TamTablero)
                    {
                        CasillasTablero[coordenada.Key] = barco.Nombre;
                    }
                }
            }
        }

        public void Disparar(Coordenada c)
        {
            if (c.Fila >= 0 && c.Columna < TamTablero
                        && c.Columna >= 0 && c.Columna < TamTablero)
            {
                foreach (var barco in Barcos)
                {
                    if (barco.CoordenadasBarco.ContainsKey(c))
                    {
                        barco.Disparo(c);
                        CoordenadasDisparadas.Add(c);
                    }
                }
            }
            else
            {
                Console.WriteLine("La coordenada " + c.ToString() + " está fuera de las dimensiones del tablero.");

            }
        }

        public string DibujarTablero()
        {
            string texto = "";
            int nColumna = 0;

            foreach (var casilla in CasillasTablero)
            {
                texto += "[" + casilla.Value + "]";
                nColumna++;

                if (nColumna == TamTablero)
                {
                    nColumna = 0;
                    texto += "\n";
                }
            }

            return texto;
        }

        public override string ToString()
        {
            string texto = "\n\n";

            foreach (var barco in Barcos)
            {
                texto += barco.ToString() + "\n";
            }

            texto += "\nCoordenadas disparadas: ";
            foreach (var coordenada in CoordenadasDisparadas)
            {
                texto += coordenada.ToString() + " ";
            }

            texto += "\nCoordenadas tocadas: ";
            foreach (var coordenada in CoordenadasTocadas)
            {
                texto += coordenada.ToString() + " ";
            }


            texto += "\n\nCASIILAS TABLERO\n";
            texto += "------\n";
            texto += DibujarTablero();

            return texto;
        }

        private void cuandoEventoTocado(object sender, TocadoArgs e)
        {
            CasillasTablero[e.CoordenadaImpacto] = e.Nombre + "_T";

            CoordenadasTocadas.Add(e.CoordenadaImpacto);

            Console.WriteLine("TABLERO: Barco [" + e.Nombre + "] tocado en Coordenada: [" + e.CoordenadaImpacto.ToString() + "]");
        }

        private void cuandoEventoHundido(object sender, HundidoArgs e)
        {
            Console.WriteLine("TABLERO: Barco [" + e.Nombre + "] hundido!!");

            Barco hundido = Barcos.First(b => b.Nombre == e.Nombre);

            if (hundido != null && BarcosEliminados.Contains(hundido))
            {
                BarcosEliminados.Add(hundido);
            }

            if (BarcosEliminados.Count == Barcos.Count)
            {
                eventoFinPartida?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
