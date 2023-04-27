using System;

namespace ConnectFour
{
    class Program
    {
        static void Main(string[] args)
        {
            // Pedir modo de juego
            Console.WriteLine("Bienvenido a Connect Four!");
            Console.WriteLine("¿Desea jugar contra la computadora? (S/N)");
            string respuestaModo = Console.ReadLine().ToUpper();
            string jugador1Nombre;
            string jugador2Nombre;
            string Juego;
            bool contraComputadora;

            // Validar respuesta de modo de juego
            if (respuestaModo == "S")
            {
                jugador1Nombre = PedirNombreJugador(1);
                jugador2Nombre = "COMPUTADORA";
                contraComputadora = true;
            }
            else
            {
                jugador1Nombre = PedirNombreJugador(1);
                jugador2Nombre = PedirNombreJugador(2);
                contraComputadora = false;
                while (jugador2Nombre.ToUpper() == "COMPUTADORA")
                {
                    Console.WriteLine("¡Error! El nombre del segundo jugador no puede ser 'COMPUTADORA'.");
                    jugador2Nombre = PedirNombreJugador(2);
                }
            }

            bool nuevoJuego;
            do
            {
                // Inicializar juego
                Juego juego = new Juego(jugador1Nombre, jugador2Nombre, contraComputadora);

                // Comenzar juego
                juego.Comenzar();

                // Preguntar si desea jugar de nuevo
                Console.WriteLine("¿Desea jugar de nuevo? (S/N)");
                string respuestaNuevoJuego = Console.ReadLine().ToUpper();
                nuevoJuego = respuestaNuevoJuego == "S";
            } while (nuevoJuego);

            // Despedida
            Console.WriteLine("¡Gracias por jugar a Connect Four! ¡Hasta pronto!");
        }

        static string PedirNombreJugador(int numeroJugador)
        {
            Console.WriteLine("Jugador {0}, por favor ingrese su nombre:", numeroJugador);
            return Console.ReadLine();
        }
    }
}

