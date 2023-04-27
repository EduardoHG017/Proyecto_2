using System;

namespace ConnectFour
{
    class Juego
    {
        // Matriz que representa el tablero
        public char[,] tablero;

        // Nombres de los jugadores
        private string jugador1Nombre;
        private string jugador2Nombre;

        // Indica si el juego se está jugando contra la computadora o no
        private bool contraComputadora;

        // Indica si el jugador1 es el turno actual
        private bool jugador1Turno;

        // Constructor
        public Juego(string jugador1Nombre, string jugador2Nombre, bool contraComputadora)
        {
            tablero = new char[6, 7];
            this.jugador1Nombre = jugador1Nombre;
            this.jugador2Nombre = jugador2Nombre;
            this.contraComputadora = contraComputadora;
            jugador1Turno = true;
        }

        // Comienza el juego
        public void Comenzar()
        {
            // Limpia el tablero
            LimpiarTablero();

            // Muestra el tablero vacío
            MostrarTablero();

            // Loop principal del juego
            while (true)
            {
                // Obtiene la columna donde quiere jugar el jugador actual
                int columna;
                if (jugador1Turno || !contraComputadora)
                {
                    columna = PedirColumna(jugador1Nombre);
                }
                else
                {
                    columna = JugarComputadora();
                    Console.WriteLine("La COMPUTADORA juega en la columna {0}", columna + 1);
                }

                // Valida que la columna sea válida y tiene espacio disponible
                if (EsColumnaValida(columna))
                {
                    int fila = ObtenerFilaDisponible(columna);
                    tablero[fila, columna] = jugador1Turno ? 'X' : 'O';
                    MostrarTablero();

                    // Verifica si el juego terminó
                    if (JuegoTerminado(fila, columna))
                    {
                        Console.WriteLine("¡{0} GANA!", jugador1Turno ? jugador1Nombre : jugador2Nombre);
                        return;
                    }

                    // Verifica si el juego terminó en empate
                    if (JuegoEmpate())
                    {
                        Console.WriteLine("¡EMPATE!");
                        return;
                    }

                    // Cambia el turno al otro jugador
                    jugador1Turno = !jugador1Turno;
                }
                else
                {
                    Console.WriteLine("Columna inválida o llena. Por favor intenta de nuevo.");
                }
            }
        }
        // Limpia el tablero
        private void LimpiarTablero()
        {
            for (int fila = 0; fila < 6; fila++)
            {
                for (int columna = 0; columna < 7; columna++)
                {
                    tablero[fila, columna] = ' ';
                }
            }
        }
        // Muestra el tablero actual
        private void MostrarTablero()
        {
            Console.Clear();
            Console.WriteLine(" 1 2 3 4 5 6 7");
            Console.WriteLine("---------------");
            for (int fila = 0; fila < 6; fila++)
            {
                Console.Write("|");
                for (int columna = 0; columna < 7; columna++)
                {
                    Console.Write("{0}|", tablero[fila, columna]);
                }
                Console.WriteLine();
                Console.WriteLine("---------------");
            }
        }

        // Obtiene la columna donde quiere jugar el jugador
        private int PedirColumna(string nombreJugador)
        {
            int columna;
            while (true)
            {
                Console.Write("{0}, por favor elige una columna (1-7): ", nombreJugador);
                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out columna))
                {
                    columna--; // ajusta la columna para que esté basada en 0 en lugar de 1
                    if (columna >= 0 && columna < 7 && !ColumnaLlena(columna))
                    {
                        return columna;
                    }
                    else
                    {
                        Console.WriteLine("Columna inválida o llena. Por favor intenta de nuevo.");
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor intenta de nuevo.");
                }
            }
        }

        // Verifica si una columna está llena
        private bool ColumnaLlena(int columna)
        {
            for (int fila = 0; fila < 6; fila++)
            {
                if (tablero[fila, columna] == ' ')
                {
                    return false;
                }
            }
            return true;
        }

        // Verifica si una columna es válida
        private bool EsColumnaValida(int columna)
        {
            if (columna >= 0 && columna < 7 && !ColumnaLlena(columna))
            {
                return true;
            }
            return false;
        }

        // Obtiene la fila disponible en una columna
        private int ObtenerFilaDisponible(int columna)
        {
            for (int fila = 5; fila >= 0; fila--)
            {
                if (tablero[fila, columna] == ' ')
                {
                    return fila;
                }
            }
            return -1; // La columna está llena
        }

        // Verifica si el juego terminó
        private bool JuegoTerminado(int fila, int columna)
        {
            // Verifica horizontalmente
            int contador = 0;
            char jugador = tablero[fila, columna];
            for (int c = 0; c < 7; c++)
            {
                if (tablero[fila, c] == jugador)
                {
                    contador++;
                    if (contador == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    contador = 0;
                }
            }

            // Verifica verticalmente
            contador = 0;
            for (int f = 0; f < 6; f++)
            {
                if (tablero[f, columna] == jugador)
                {
                    contador++;
                    if (contador == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    contador = 0;
                }
            }

            // Verifica diagonalmente (hacia arriba y a la izquierda)
            contador = 0;
            int c1 = columna;
            int f1 = fila;
            while (c1 > 0 && f1 > 0)
            {
                c1--;
                f1--;
            }
            while (c1 < 7 && f1 < 6)
            {
                if (tablero[f1, c1] == jugador)
                {
                    contador++;
                    if (contador == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    contador = 0;
                }
                c1++;
                f1++;
            }

            // Verifica diagonalmente (hacia arriba y a la derecha)
            static bool VerificarDiagonalArribaDerecha(int fila, int columna, char jugador)
            {
                int fichasConsecutivas = 1;
                int i = fila - 1;
                int j = columna + 1;
                while (i >= 0 && j < 7 && tablero[i, j] == jugador)
                {
                    fichasConsecutivas++;
                    i--;
                    j++;
                }

                i = fila + 1;
                j = columna - 1;

                while (i < 6 && j >= 0 && tablero[i, j] == jugador)
                {
                    fichasConsecutivas++;
                    i++;
                    j--;
                }

                return fichasConsecutivas >= 4;
            }

            // Verifica diagonalmente (hacia abajo y a la derecha)
            private bool VerificarDiagonalAbajoDerecha(int fila, int columna, char jugador)
            {
                int fichasConsecutivas = 1;
                int i = fila + 1;
                int j = columna + 1;
                while (i < 6 && j < 7 && tablero[i, j] == jugador)
                {
                    fichasConsecutivas++;
                    i++;
                    j++;
                }

                i = fila - 1;
                j = columna - 1;

                while (i >= 0 && j >= 0 && tablero[i, j] == jugador)
                {
                    fichasConsecutivas++;
                    i--;
                    j--;
                }

                return fichasConsecutivas >= 4;
            }

            // Verifica si el juego ha terminado en empate
            private bool JuegoEmpate()
            {
                for (int columna = 0; columna < 7; columna++)
                {
                    if (EsColumnaValida(columna))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}

















