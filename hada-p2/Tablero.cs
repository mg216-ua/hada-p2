using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Tablero
    {
        //Difinición private de entero para entender el tamaño de tablero
        private int _tamTablero;
        public int TamTablero
        {
            get { return _tamTablero; }
            set
            {
                //Si el valor es mayor o igual que 4 o menor o igual que 9
                //se puede ser los valores definidos solo en este rango
                if (value >= 4 && value <= 9)
                {
                    _tamTablero = value;
                }
            }
        }

        //Las listas privadas definidas de Coordenadas introducidas, si son Disparadas o Tocadas
        //Tambien los lostas de barcos diferentes
        private List<Coordenada> CoordenadasDisparadas;
        private List<Coordenada> CoordenadasTocadas;
        private List<Barco> Barcos;
        private List<Barco> BarcosEliminados;
        private Dictionary<Coordenada, string> CasillasTablero;

        //Evento que se lanza al acabar la partida
        public event EventHandler<EventArgs> eventoFinPartida;

        //Contructor
        public Tablero(int tamTablero, List<Barco> barcos)
        {
            this.TamTablero = tamTablero;
            this.Barcos = barcos;

            CoordenadasDisparadas = new List<Coordenada>();
            CoordenadasTocadas = new List<Coordenada>();
            BarcosEliminados = new List<Barco>();

            //Crear la lista de casillas
            CasillasTablero = new Dictionary<Coordenada, string>();
            //Inicializar la lista de casillas
            inicializarCasillasTablero();

            //Añadimos el evento para cada uno de los barcos en el codigo
            foreach (var barco in Barcos)
            {
                barco.eventoTocado += cuandoEventoTocado;
                barco.eventoHundido += cuandoEventoHundido;
            }

        }

        //Iniciamos casillas de tablero
        private void inicializarCasillasTablero()
        {
            CasillasTablero.Clear();

            //Recorremos las posiciones del tablero
            for (int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    //Asignamos una casulla con valor de AGUA
                    CasillasTablero[new Coordenada(i, j)] = "AGUA";
                }
            }

            //Recoremos la lista de barcos
            foreach (var barco in this.Barcos)
            {
                //Para cada barco recorremos la lista de coordenadas
                foreach (var coordenada in barco.CoordenadasBarco)
                {
                    //Si la fila y la columna estan dentro
                    if (coordenada.Key.Fila >= 0 && coordenada.Key.Columna < TamTablero
                        && coordenada.Key.Columna >= 0 && coordenada.Key.Columna < TamTablero)
                    {
                        CasillasTablero[coordenada.Key] = barco.Nombre;
                    }
                }
            }
        }

        //Metodo disparar
        public void Disparar(Coordenada c)
        {
            if (c.Fila >= 0 && c.Columna < TamTablero
                        && c.Columna >= 0 && c.Columna < TamTablero)
            {
                //Añadimos a la lista de coordenadas disparadas
                CoordenadasDisparadas.Add(c);

                //Para cada barco de la lista comprobar que esta en esta fila columna
                foreach (var barco in Barcos)
                {
                    //Si el barco tiene la coordenada del parametro
                    if (barco.CoordenadasBarco.ContainsKey(c))
                    {
                        barco.Disparo(c);
                    }
                }
            }
            else
            { //En caso contrario, definimos siguiente mensaje
                Console.WriteLine("La coordenada " + c.ToString() + " está fuera de las dimensiones del tablero.");

            }
        }

        //Metodo debujar tablero
        public string DibujarTablero()
        {
            string texto = "";
            int nColumna = 0;

            foreach (var casilla in CasillasTablero)
            {
                texto += "[" + casilla.Value + "]";
                //Incrementamos numero de columna
                nColumna++;

                //Si el numero de columna es igual al tamaño de tablero ==> cambiamos fila
                if (nColumna == TamTablero)
                {
                    nColumna = 0;
                    texto += "\n";
                }
            }

            return texto;
        }

        //Metodo ToString para escribir el mensaje necesario de juego en consola
        public override string ToString()
        {
            string texto = "\n\n";

            //Información de cada barco
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

            //El tablero
            texto += "\n\nCASIILAS TABLERO\n";
            texto += "------\n";
            texto += DibujarTablero();

            return texto;
        }

        //Manejador del evento cuandoEventoTocado
        private void cuandoEventoTocado(object sender, TocadoArgs e)
        {
            //Marcamos la casilla del evento como tocada
            CasillasTablero[e.CoordenadaImpacto] = e.Nombre + "_T";

            //Añadimos a la lista de casillas tocadas
            CoordenadasTocadas.Add(e.CoordenadaImpacto);

            //Monstramos el mensaje por pantalla
            Console.WriteLine("TABLERO: Barco [" + e.Nombre + "] tocado en Coordenada: [" + e.CoordenadaImpacto.ToString() + "]");
        }

        //Manejador del evento cuandoEventoHundido
        private void cuandoEventoHundido(object sender, HundidoArgs e)
        {
            //Monstramos el mensaje
            Console.WriteLine("TABLERO: Barco [" + e.Nombre + "] hundido!!");

            //Recuperamos los datos del barco hundido de la lista de barcos de la aplicacion
            //Buscamos primer barco que tenga el mismo nombre que el barco de evento e
            Barco hundido = Barcos.First(b => b.Nombre == e.Nombre);

            //Si el barco no esta en la lista de barcos eliminados
            if (hundido != null && BarcosEliminados.Contains(hundido))
            {
                BarcosEliminados.Add(hundido);
            }

            //Si el numero de barcos hundidos = numero de barcos totales
            if (BarcosEliminados.Count == Barcos.Count)
            {
                //Invocar el evento de fin de partida
                eventoFinPartida?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
