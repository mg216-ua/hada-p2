using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Game
    {
        //Booleano privado de terminar la partida
        private bool finPartida;
        //Valor statico privado de definir los valores numericos de manera random
        private static Random rand = new Random();

        //Constructor de juego
        public Game()
        {
            finPartida = false;
            gameLoop();
        }

        //Booleano para definir si la coordenada de barco es erronea (imposible)
        private Boolean barcosCoordenadaErronea(List<Barco> listaBarcos, int longitud)
        {
            //Recorro la lista de Barcos para comprobar si sea coordenada repetida
            foreach (var barco in listaBarcos)
            {
                //Comprobamos las coordenadas de cada coordenadas de los barcos
                foreach(var coordenada in barco.CoordenadasBarco)
                {
                    //Si la fila al suma la longitud se sale de tablero o si la columna al sumar se sale
                    if (coordenada.Key.Fila >= longitud || coordenada.Key.Columna >= longitud)
                    {
                        return true;
                    }

                    //Si la fila y columna esta dentro del ranfo revisamos si esta repetida en algun barco
                    foreach (var barcoIt in listaBarcos)
                    {
                        foreach (var coordenadaIt in barcoIt.CoordenadasBarco)
                        {
                            //Si la coordenada del barco es igual que la que hemos generado
                            if (coordenada.Key.Equals(coordenadaIt.Key) && !barco.Equals(barcoIt))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private Coordenada coordenadaAleatoria(List<Barco> listaBarcos, int longitud)
        {
            bool repetir;
            int fila;
            int columna;
            Coordenada c;

            //Crear un numero aleatorio mientras salga una coordenada repetida
            do
            {
                repetir = false;

                //Generar una fila y columna aleatoria
                fila = rand.Next(0, longitud);
                columna = rand.Next(0, longitud);

                //Crear una coordenada a partir de la fila y columna aleatoria
                c = new Coordenada(fila, columna);

                //Recorro la lista de barcos para comprobar si esa coordenada esta repetida
                foreach (var barco in listaBarcos)
                {
                    foreach (var coordenada in barco.CoordenadasBarco)
                    {
                        //Si la coordenada del barco es igual que la que hemos generado
                        if (coordenada.Key.Equals(c))
                        {

                        }
                    }
                }
            }while (repetir);

            return c;
        }

        private void gameLoop()
        {
            int longitud; //Valor entero de longitud

            //Pedimos la logitud del tablero
            Console.WriteLine("Introduce longitud de tablero: ");

            //Itentamos convertirlo en entero y guardando el valor en la variable longitud
            bool correcto = int.TryParse(Console.ReadLine(), out longitud);

            if (correcto)
            {
                List<Barco> listaBarcos = new List<Barco>();

                do
                {
                    listaBarcos.Clear();

                    //Crear una lista con 3 barcos
                    Coordenada c1 = coordenadaAleatoria(listaBarcos, longitud);
                    listaBarcos.Add(new Barco("THOR", 3, 'h', c1));
                    Coordenada c2 = coordenadaAleatoria(listaBarcos, longitud);
                    listaBarcos.Add(new Barco("LOKI", 2, 'v', c2));
                    Coordenada c3 = coordenadaAleatoria(listaBarcos, longitud);
                    listaBarcos.Add(new Barco("MAYA", 4, 'h', c3));

                } while (barcosCoordenadaErronea(listaBarcos, longitud));

                //Crear nuevo tablero y añadiendole los barcos
                Tablero tablero = new Tablero(longitud, listaBarcos);

                //Añadirle el evento fin de partida
                tablero.eventoFinPartida += cuandoEventoFinPartida;

                int fila, col;

                //Bucle que juege mientras no se haya producido el final de la partida
                while (!finPartida)
                {
                    //Dibujamos para cada interacción del tablero para comprobar como se encuentra
                    Console.WriteLine(tablero.ToString());

                    //Pedimos la coordenada sobre la que dispara
                    Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para Salir)");
                    string coordStr = Console.ReadLine();

                    if (coordStr.Equals("S") || coordStr.Equals("s"))
                    {
                        finPartida = true;
                    }
                    else
                    {
                        //Separar los datos de la coordenada por la coma
                        string[] fila_col = coordStr.Split(',');

                        //Si array de fila-col tiene dos elementos y cada uno de los elementos los he podido convertir a entero
                        if (fila_col.Length == 2 && int.TryParse(fila_col[0], out fila) && int.TryParse(fila_col[1], out col))
                        {
                            //Crear la coordenada con datos de fila y columna
                            Coordenada coordDisparo = new Coordenada(fila, col);
                            //Disparar al tablero
                            tablero.Disparar(coordDisparo);
                        }
                        else
                        {
                            Console.WriteLine("Formato incorrecto. El formato debe ser NUMERO,NUMERO");
                        }
                    }
                }

                //Dibujamos el tablero una vez finalizada la partida
                Console.WriteLine(tablero.ToString());
            }
            else
            {
                Console.WriteLine("Formato incorrecto. El formato debe ser NUMERO,NUMERO");
            }
        }


        //privado metodo de terminar la partida segun el evento e
        private void cuandoEventoFinPartida(object sender, EventArgs e)
        {
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
        }
    }
}
