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
        // Boolenao que verifica la partida
        private bool finPartida;

        // Listas de strings con posibles nombres de los barcos
        //      Cada lista es una entidad separada con nombres de facción asociada
        private static string[] names_USN = { "WASP", "LANG", "HULL", "FARR", "BUCK", "IOWA", "GUAM", "SIMS", "RENO", "DACE" }; // EEUU
        private static string[] names_RoyalNavy = { "DUKE", "KITE", "CAMP", "GREY", "VERN", "HERO", "HOWE", "HOOD", "FIJI", "LION" }; // Reino unido
        private static string[] names_Kriegsmarine = { "LUTZ", "ZARA", "WOLF", "U-99", "KÖLN", "THOR", "TOGO", "ODIN", "ÄGIR", "ROON" }; // Alemania
        private static string[] names_ImperialNavy = { "HIEI", "FUSŌ", "TOSA", "KAGA", "HIYŌ", "KUMA", "AOBA", "MAYA", "OITE", "YURA" }; // Japón
        private static string[][] names = { names_USN, names_RoyalNavy, names_Kriegsmarine, names_ImperialNavy }; // Lista con las listas

        private static Random rand = new Random(); // Rand usado para la generación
        
        // Parámetros del juego
        private static List<int> pickedNames=new List<int>();
        private static List<Coordenada> pickedCoordenadas = new List<Coordenada>();

        // Bucle del juego
        private void gameLoop()
        {
            int tamTablero = rand.Next(4, 10); // Tablero aleatorio entre 4 y 10
            Barco.tablero = tamTablero; // Le decimos a los barcos el tamaño de tablero

            Tablero tablero = new Tablero(tamTablero, generarBarcos(tamTablero)); // Creamos tablero con barcos generados
            tablero.eventoFinPartida += cuandoEventoFinPartida; // Vinculamos event handler

            while (true) {
                if (finPartida) // Si partida finalizó, salimos
                {
                    break;
                }
                // Imprimimos cositas
                Console.WriteLine(tablero.ToString());
                // Aquí dejo la implementación sin color comentada, para activalra simplemente comentar la opción de color y quitar comentario a la original
                //Console.WriteLine(tablero.DibujarTablero()); // Sin color
                tablero.DibujarTableroConColor(); // Con color
                string usr_input = Console.ReadLine(); // input de usuario
                int usr_fila, usr_columna;

                if (!finPartida && !(usr_input == "s" || usr_input == "S")) // Si no es final de partida y el input no es "s" o "S"
                {
                    // Con regex podemos establecer un patrón que tiene que seguir el input para ser validado ["número"+"coma"+"número"]
                    Match match = Regex.Match(usr_input, @"^\s*(\d)\s*,\s*(\d)\s*$"); // Dejaré mi comentario original: C# regex is GOATed
                    if (match.Success) { // Si el input sigue patrón estblecido:
                        usr_fila = int.Parse(match.Groups[1].Value); // hacemos parse deprimer valor a fila
                        usr_columna = int.Parse(match.Groups[2].Value); // y segundo a columna
                        tablero.Disparar(new Coordenada(usr_fila,usr_columna)); // Luego disparamos la coordenada
                    }
                }
                else
                {
                    break;
                }
                // El hehco de que no aparecen los textos de algunos avisos se debe a que se limpian muy rapido con el console clear.
                //      Se puede simplemente quitar el console.Clear() y entonces saldrán todos los mensajes intermedios. Un chiquito problema que no me da la vida solucionar
                Console.Clear();
            }
        }

        // Seleccionador de nombre para el barco, recibe como parámetro la flota/facción a que pertenece
        private static string nombreBarco(int fleet)
        {
            while(true){ // El bucle se repite hasta encontrar un noombre válido
                int ship = rand.Next(0, names[fleet].Length); // Seleccióna un nombre aleatorio de la lista asociada a la facción
                if (!pickedNames.Contains(ship)) // Si la lista de nombre ya escogidos por otros barcos no contien el nombre elegido:
                {
                    pickedNames.Add(ship); // Añade el nombre a la lista
                    return names[fleet][ship]; // Devuelve el nombre
                }
            }
        }

        // Generador de barcos aleatorio, no me da tiempo a refactorizar, tengomuchas asignaturas este trimestre
        private List<Barco> generarBarcos(int tamTablero)
        {
            List<Barco> barcos = new List<Barco>{};
            pickedCoordenadas.Clear(); // Por sea caso limpiamos  las coordenadas oupadas
            int numBarcos;
            if (tamTablero>=6) // Si el tablero es grande, podemos acomodar entre 4 y 5 barcos
            {
                numBarcos = rand.Next(4, 6);
            }
            else // Si el tablero es pequeño, acomodamos 3 barcos
            {
                numBarcos = 3;
            }
            int fleet = rand.Next(0, 4); // Selección de la flota/facción      

            // Generamos i barcos
            for (int i = 0; i<numBarcos ;i++)
            {
                // Ya que cada barco es de longitud 1+iteración (ej: barco 1 delongitud 1 (0+1), 2 de longitud 2 (1+1), etc.)
                int longitudBarco = i + 1;

                char orient; // Orientación aleatoria
                int orientacion = rand.Next(0, 2);
                if (orientacion==0)
                {
                    orient = 'h';
                }
                else { orient = 'v'; }

                int fila, columna;
                List<Coordenada> coordBarco = new List<Coordenada>();
                bool valid = false; // asumimos que la coordenada es inválida
                while (true)
                {
                    valid = true; // Ahora asumimos que sí lo es
                    if (orient == 'v') // COmprobación para barcos verticales
                    {
                        // Temporalmente creamos la lista de coordenadas que ocuparía el barco generado
                        fila = rand.Next(0, tamTablero-longitudBarco+1);
                        columna = rand.Next(0, tamTablero);
                        coordBarco.Clear();
                        for (int j = 0; j < longitudBarco; j++)
                        {
                            coordBarco.Add(new Coordenada(fila + j, columna));
                        }
                    }
                
                    if (orient == 'h') // Barcos horizontales
                    {
                        fila = rand.Next(0, tamTablero);
                        columna = rand.Next(0, tamTablero-longitudBarco+1);
                        coordBarco.Clear();
                        for (int j = 0; j < longitudBarco; j++)
                        {
                            coordBarco.Add(new Coordenada(fila, columna + j));
                        }
                        
                    }
                
                    // Verificamos la validez del barco generado
                    foreach (Coordenada cord in coordBarco)
                    {
                        foreach (Barco b in barcos)
                        {
                            if (b.CoordenadasBarco.ContainsKey(cord)) // Si las coordenadas de algun barco anterior, contien coordenada alguna del nuevo
                            {
                                valid = false; // Es inválido
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
                        break; // Si el barco es válido, salimos del bucle de la creación del barco particular
                    }
                }

                // Ya no se usa, antes se usó para guardar las coordenadas usadas por lo barcos en vez de recorrer con foreach
                foreach (Coordenada  cord in coordBarco)
                {
                    pickedCoordenadas.Add(cord);
                }

                // Creamos y añadimos el barco a la lista de barcos
                Barco barco = new Barco(nombreBarco(fleet), longitudBarco, orient, coordBarco[0]);
                barcos.Add(barco);
            }

            return barcos; // Devolvemos la lista completa de barcos
        }

        // Event handler del game over
        private void cuandoEventoFinPartida(object sender, EventArgs e)
        {
            Console.Clear(); // Limpiamos consola
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true; // terminamos partida
        }

        // Constuctor del juego
        public Game() 
        {
            this.finPartida = false;
            this.gameLoop();
        }
    }
}
