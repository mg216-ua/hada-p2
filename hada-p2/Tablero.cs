using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Tablero
    {
        // Evento asociado al tablero
        public event EventHandler<EventArgs> eventoFinPartida;

        // Homónimo privado
        private int tamtablero;
        public int TamTablero {
            get 
            { return this.tamtablero; }
            private set
            { if (value >= 4 && value <= 9){ this.tamtablero = value;} }
        }
        // Parámetros públicos
        public List<Coordenada> coordenadasDisparadas { get; private set; }
        public List<Coordenada> coordenadasTocadas { get; private set; }
        public List<Barco> barcos {  get; private set; }
        public List<Barco> barcosEliminados { get; private set; }
        public Dictionary<Coordenada, string> casillasTablero { get; private set; }

        /// <summary>
        /// COnstructor del tablero
        /// </summary>
        /// <param name="_TamTablero">Tamaño del tablero (0<X<9)</param>
        /// <param name="_barcos">Lista con barcos</param>
        public Tablero(int _TamTablero, List<Barco> _barcos)
        {
            // Definición de parámetross
            this.TamTablero= _TamTablero;
            this.barcos = _barcos;
            this.coordenadasTocadas = new List<Coordenada>{};
            this.coordenadasDisparadas = new List<Coordenada>{};
            this.barcosEliminados = new List<Barco>{};
            this.casillasTablero = new Dictionary<Coordenada, string>();

            // A cada barco del tablero, vinculamos handlers de sus eventos
            foreach (Barco barco in this.barcos)
            {
                barco.eventoTocado += cuandoEventoTocado;
                barco.eventoHundido += cuandoEventoHundido;
            }

            // Inicializamos casillas de tablero
            inicializaCasillasTablero();
        }

        // Función auxiliar de iniciación de tablero
        private void inicializaCasillasTablero()
        {
            for (int i = 0;i<this.TamTablero ;i++) // Recorremos el Eje vertical
            {
                for (int j=0; j < this.TamTablero; j++) // Recorremos el Eje horizontal
                {
                    bool barquito = false; // Asumimos que en la casilla no hay barco
                    Coordenada _cord = new Coordenada(i, j);
                    foreach (Barco barco in this.barcos) // Para cada barco del tablero:
                    {
                        if (barco.CoordenadasBarco.ContainsKey(_cord)) // Si el barco contiene dicha coordenada del tablero:
                        {
                            // Asignamos la casilla al barco
                            this.casillasTablero[_cord] = barco.Nombre;
                            barquito = true;
                            break;
                        }
                    }
                    if (!barquito) // Si no hay barco en la casilla
                    {
                        // Asignamos agua
                        this.casillasTablero[_cord] = "AGUA";
                    }
                }
            }
        }

        // FUnción de disparo
        public void Disparar(Coordenada cord)
        {
            // Si la coordenada está fuera de los límites del tablero
            if ((cord.Columna>=this.TamTablero||cord.Columna<0)||(cord.Fila>=this.TamTablero||cord.Fila<0))
            {
                // Decimos que es un error e ignoramos el input
                Console.WriteLine("La coordenada "+cord.ToString()+" está fuera de las dimensiones del tablero.");
                return;
            }
            // Si no, añadimos la coordenada a disparadas
            this.coordenadasDisparadas.Add(cord);
            // Y comprobamos si la coordenada tien un barco asociado
            foreach (Barco barco in this.barcos)
            {
                // En caso positivo:
                if (barco.CoordenadasBarco.ContainsKey(cord))
                {
                    // Le decimos al barco que le han tocado
                    barco.Disparo(cord);
                }
            }
        }

        // Mi versión de dibuja tablero que no devuelve string (ya que no funcióna) si no utiliza propiedades del terminal
        //      para imprimir colores
        public void DibujarTableroConColor()
        {
            for (int i = 0; i < tamtablero; i++)
            {
                for (int j = 0; j < tamtablero; j++)
                {
                    Coordenada cord = new Coordenada(i, j);
                    string casilla;
                    if (!casillasTablero.TryGetValue(cord, out casilla)) // Extracción de nombre de la casilla asociada
                    {
                        casilla = "AGUA"; // Si no es barco, asignamos agua
                    }
                    if (casilla == "AGUA") // Agua
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("[" + casilla + "]");
                        Console.ResetColor();
                    }
                    else // Barcos
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[" + casilla + "]");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
        }

        // Función que dibuja tablero, tal y como nos la piden implementar
        public string DibujarTablero()
        {
            string str=""; // Cadena base para construir
            for (int i = 0; i<this.tamtablero;i++) // Recorremos filas
            {
                for (int j = 0; j < this.tamtablero; j++) // Recorremos columnas
                {
                    Coordenada cord = new Coordenada(i,j);
                    string casilla; // Extraemos el nombre asociado a la coordenada (AGUA/BARCO)
                    if (!this.casillasTablero.TryGetValue(cord, out casilla)) // Aquí se extrae
                    {
                        // Dejo aquí el intento fallido de implementar color con uno de los métodos simples:
                        //      añadiendo \1b[0m] supuestamente debería de indicar colorización del segmento...
                        casilla = "\x1b[0m]" + "AGUA"+ "\x1b[0m]";// Si no hemos podido extraer el nombre del barco, entonces significa que hay agua
                    }str += "["+casilla+"]"; // Si hemos podido extraer nombre de barco, lo impimimos
                }
                str += "\n";
            }
            return str;
        }

        // To string asociado
        public override string ToString()
        {
            string str="";
            foreach (Barco barco in this.barcos) // Para cadabarco, imprimimos coordenadas disparadas
            {
                str+=barco.ToString()+"\n";
            } str += "\nCoordenadas disparadas:";
            foreach (Coordenada cord in this.coordenadasDisparadas)
            {
                str+=cord.ToString(); // ... coordenadas tocadas
            } str += "\nCoordenadas tocadas:";
            foreach (Coordenada cord in this.coordenadasTocadas)
            {
                str+=cord.ToString();
            } str += "\nCASILLAS TABLERO:\n-------\n";

            return str;
        }

        // Evento de barco tocado
        private void cuandoEventoTocado(object sender, TocadoArgs e)
        {
            // Si el tablero contiene la casilla disparada
            if (this.casillasTablero.ContainsKey(e.cord))
            {
                casillasTablero[e.cord] = e.nombre + "_T"; // Añadimos tag de toque al nombre asociado
            }this.coordenadasTocadas.Add(new Coordenada(e.cord)); // añadismo la coordenada a la lista de coordenadas tocadas
            
            Console.WriteLine("TABLERO: Barco ["+e.nombre+"] tocado en Coordenada: ["+e.cord.ToString()+"]");
        }

        // Event handler del hundimiento(¿? existe la palabra hundimiento?) de un barco
        private void cuandoEventoHundido(object sender, HundidoArgs e)
        {
            Console.WriteLine("TABLERO: Barco ["+e.nombre+"] hundido!!");
            this.barcosEliminados.Add(this.barcos.First(b => b.Nombre == e.nombre)); // Añadimos el barco a la lista de barcos hundidos
            if(this.barcos.All(b => b.hundido())) // Si no quedan barcos vivos / Todos los barcos hundidos
            {
                eventoFinPartida.Invoke(this, EventArgs.Empty); // Invocamos evento de game over
            }
        }
    }
}
