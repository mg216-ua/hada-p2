using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Game
    {
        private bool finPartida;

        public Game()
        {
            finPartida = false;
            gameLoop();
        }

        private void gameLoop()
        {
            List<Barco> listaBarcos = new List<Barco>();
            listaBarcos.Add(new Barco("THOR", 3, 'h', new Coordenada(0, 0)));
            listaBarcos.Add(new Barco("LOKI", 2, 'v', new Coordenada(2, 4)));
            listaBarcos.Add(new Barco("MAYA", 4, 'h', new Coordenada(5, 1)));

            Tablero tablero = new Tablero(6, listaBarcos);

            //Añadirle el evento fin de partida
            //tablero.eventoFinPartida += cuandoEventoFinPartida;

            int fila, col;

            while (!finPartida)
            {
                Console.WriteLine(tablero.ToString());

                Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para Salir)");
                string coordStr = Console.ReadLine();

                if (coordStr.Equals("S"))
                {
                    finPartida = true;
                }
                else
                {
                    string[] fila_col = coordStr.Split(',');

                    if (fila_col.Length == 2 && int.TryParse(fila_col[0], out fila) && int.TryParse(fila_col[1], out col))
                    {

                        Coordenada coordDisparo = new Coordenada(fila, col);
                        tablero.Disparar(coordDisparo);
                    }
                    else
                    {
                        Console.WriteLine("Formato incorrecto. El formato debe ser NUMERO,NUMERO");
                    }
                }
            }
        }

        private void cuandoEventoFinPartida(EventArgs e)
        {
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
        }
    }
}
