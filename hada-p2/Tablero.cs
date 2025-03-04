using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Tablero
    {

        public event EventHandler<EventArgs> eventoFinPartida;

        private int tamtablero;
        public int TamTablero {
            get 
            { return this.tamtablero; }
            private set
            { if (value >= 4 && value <= 9){ this.tamtablero = value;} }
        }
        public List<Coordenada> coordenadasDisparadas { get; private set; }
        public List<Coordenada> coordenadasTocadas { get; private set; }
        public List<Barco> barcos {  get; private set; }
        public List<Barco> barcosEliminados { get; private set; }
        public Dictionary<Coordenada, string> casillasTablero { get; private set; }

        public Tablero(int _TamTablero, List<Barco> _barcos)
        {
            this.TamTablero= _TamTablero;
            this.barcos = _barcos;
            this.coordenadasTocadas = new List<Coordenada>{};
            this.coordenadasDisparadas = new List<Coordenada>{};
            this.barcosEliminados = new List<Barco>{};
            this.casillasTablero = new Dictionary<Coordenada, string>();

            foreach (Barco barco in this.barcos)
            {
                barco.eventoTocado += cuandoEventoTocado;
                barco.eventoHundido += cuandoEventoHundido;
            }

            inicializaCasillasTablero();
        }

        private void inicializaCasillasTablero()
        {
            for (int i = 0;i<this.TamTablero ;i++)
            {
                for (int j=0; j < this.TamTablero; j++)
                {
                    bool barquito = false;
                    Coordenada _cord = new Coordenada(i, j);
                    foreach (Barco barco in this.barcos)
                    {
                        if (barco.CoordenadasBarco.ContainsKey(_cord))
                        {
                            this.casillasTablero[_cord] = barco.Nombre;
                            barquito = true;
                            break;
                        }
                    }
                    if (!barquito)
                    {
                        this.casillasTablero[_cord] = "AGUA";
                    }
                }
            }
        }

        public void Disparar(Coordenada cord)
        {
            if ((cord.Columna>=this.TamTablero||cord.Columna<0)||(cord.Fila>=this.TamTablero||cord.Fila<0))
            {
                Console.WriteLine("La coordenada "+cord.ToString()+" está fuera de las dimensiones del tablero.");
                return;
            }
            this.coordenadasDisparadas.Add(cord);
            foreach (Barco barco in this.barcos)
            {
                if (barco.CoordenadasBarco.ContainsKey(cord))
                {
                    barco.Disparo(cord);
                }
            }
        }

        public void DibujarTableroConColor()
        {
            for (int i = 0; i < tamtablero; i++)
            {
                for (int j = 0; j < tamtablero; j++)
                {
                    Coordenada cord = new Coordenada(i, j);
                    string casilla;
                    if (!casillasTablero.TryGetValue(cord, out casilla))
                    {
                        casilla = "AGUA";
                    }
                    if (casilla == "AGUA")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("[" + casilla + "]");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[" + casilla + "]");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
        }


        public string DibujarTablero()
        {
            string str="";
            for (int i = 0; i<this.tamtablero;i++)
            {
                for (int j = 0; j < this.tamtablero; j++)
                {
                    Coordenada cord = new Coordenada(i,j);
                    string casilla;
                    if (!this.casillasTablero.TryGetValue(cord, out casilla))
                    {
                        casilla = "\x1b[0m]" + "AGUA"+ "\x1b[0m]";
                    }str += "["+casilla+"]";
                }
                str += "\n";
            }
            return str;
        }

        public override string ToString()
        {
            string str="";
            foreach (Barco barco in this.barcos)
            {
                str+=barco.ToString()+"\n";
            } str += "\nCoordenadas disparadas:";
            foreach (Coordenada cord in this.coordenadasDisparadas)
            {
                str+=cord.ToString();
            } str += "\nCoordenadas tocadas:";
            foreach (Coordenada cord in this.coordenadasTocadas)
            {
                str+=cord.ToString();
            } str += "\nCASILLAS TABLERO:\n-------\n";

            return str;
        }

        private void cuandoEventoTocado(object sender, TocadoArgs e)
        {
            if (this.casillasTablero.ContainsKey(e.cord))
            {
                casillasTablero[e.cord] = e.nombre + "_T";
            }this.coordenadasTocadas.Add(new Coordenada(e.cord));
            
            Console.WriteLine("TABLERO: Barco ["+e.nombre+"] tocado en Coordenada: ["+e.cord.ToString()+"]");
        }

        private void cuandoEventoHundido(object sender, HundidoArgs e)
        {
            Console.WriteLine("TABLERO: Barco ["+e.nombre+"] hundido!!");
            this.barcosEliminados.Add(this.barcos.First(b => b.Nombre == e.nombre));
            if(this.barcos.All(b => b.hundido()))
            {
                eventoFinPartida.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
