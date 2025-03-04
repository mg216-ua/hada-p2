using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// All ships sunk event is bugged

namespace Hada
{
    internal class Game
    {
        private bool finPartida;

        private static string[] names_USN = { "WASP", "LANG", "HULL", "FARR", "BUCK", "IOWA", "GUAM", "SIMS", "RENO", "DACE" };
        private static string[] names_RoyalNavy = { "DUKE", "KITE", "CAMP", "GREY", "VERN", "HERO", "HOWE", "HOOD", "FIJI", "LION" };
        private static string[] names_Kriegsmarine = { "LUTZ", "ZARA", "WOLF", "U-99", "KÖLN", "THOR", "TOGO", "ODIN", "ÄGIR", "ROON" };
        private static string[] names_ImperialNavy = { "HIEI", "FUSŌ", "TOSA", "KAGA", "HIYŌ", "KUMA", "AOBA", "MAYA", "OITE", "YURA" };
        private static string[][] names = { names_USN, names_RoyalNavy, names_Kriegsmarine, names_ImperialNavy };
        private static Random rand = new Random();
        private static List<int> pickedNames=new List<int>();
        private static List<Coordenada> pickedCoordenadas = new List<Coordenada>();

        private void gameLoop()
        {
            int tamTablero = rand.Next(4, 10);
            Barco.tablero = tamTablero;

            Tablero tablero = new Tablero(tamTablero, generarBarcos(tamTablero));
            tablero.eventoFinPartida += cuandoEventoFinPartida;

            while (true) {
                if (finPartida)
                {
                    break;
                }
                Console.WriteLine(tablero.ToString());
                tablero.DibujarTableroConColor();
                string usr_input = Console.ReadLine();
                int usr_fila, usr_columna;

                if (!finPartida && !(usr_input == "s" || usr_input == "S"))
                {
                    Match match = Regex.Match(usr_input, @"^\s*(\d)\s*,\s*(\d)\s*$"); // C# regex is GOATed
                    if (match.Success) {
                        usr_fila = int.Parse(match.Groups[1].Value);
                        usr_columna = int.Parse(match.Groups[2].Value);
                        tablero.Disparar(new Coordenada(usr_fila,usr_columna));
                    }
                }
                else
                {
                    break;
                }
                Console.Clear();
            }
        }

        private static string nombreBarco(int fleet)
        {
            while(true){
                int ship = rand.Next(0, names[fleet].Length);
                if (!pickedNames.Contains(ship))
                {
                    pickedNames.Add(ship);
                    return names[fleet][ship];
                }
            }
        }

        private List<Barco> generarBarcos(int tamTablero)
        {
            List<Barco> barcos = new List<Barco>{};
            pickedCoordenadas.Clear();
            int numBarcos;
            if (tamTablero>=6)
            {
                numBarcos = rand.Next(4, 6);
            }
            else
            {
                numBarcos = 3;
            }
            int fleet = rand.Next(0, 4);            

            for (int i = 0; i<numBarcos ;i++)
            {
                int longitudBarco = i + 1;
                char orient;
                int orientacion = rand.Next(0, 2);
                if (orientacion==0)
                {
                    orient = 'h';
                }
                else { orient = 'v'; }
                int fila, columna;
                List<Coordenada> coordBarco = new List<Coordenada>();
                bool valid = false;
                while (true)
                {
                    valid = true;
                    if (orient == 'v')
                    {
                        fila = rand.Next(0, tamTablero-longitudBarco+1);
                        columna = rand.Next(0, tamTablero);
                        coordBarco.Clear();
                        for (int j = 0; j < longitudBarco; j++)
                        {
                            coordBarco.Add(new Coordenada(fila + j, columna));
                        }
                    }
                
                    if (orient == 'h')
                    {
                        fila = rand.Next(0, tamTablero);
                        columna = rand.Next(0, tamTablero-longitudBarco+1);
                        coordBarco.Clear();
                        for (int j = 0; j < longitudBarco; j++)
                        {
                            coordBarco.Add(new Coordenada(fila, columna + j));
                        }
                        
                    }
                

                    foreach (Coordenada cord in coordBarco)
                    {
                        foreach (Barco b in barcos)
                        {
                            if (b.CoordenadasBarco.ContainsKey(cord))
                            {
                                valid = false;
                                break;
                            }
                        }
                        if (!valid)
                        {
                            break;
                        }
                    }
                    if (valid)
                    {
                        break;
                    }
                }

                foreach (Coordenada  cord in coordBarco)
                {
                    pickedCoordenadas.Add(cord);
                }

                Barco barco = new Barco(nombreBarco(fleet), longitudBarco, orient, coordBarco[0]);
                barcos.Add(barco);
            }

            return barcos;
        }

        private void cuandoEventoFinPartida(object sender, EventArgs e)
        {
            Console.Clear();
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
        }

        public Game() 
        {
            this.finPartida = false;
            this.gameLoop();
        }
    }
}
