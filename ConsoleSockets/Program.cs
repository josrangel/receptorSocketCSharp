using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;               //   Paso 1
using System.Net.Sockets;       //   Paso 1

namespace ConsoleSockets
{
    class Program
    {
        static void Main(string[] args)
        {
            Conectar();
        }

        public static void Conectar()
        {
            byte[] ByRec;
            Socket miPrimerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint miDireccion = new IPEndPoint(IPAddress.Any, 1234);

            try
            {
                miPrimerSocket.Bind(miDireccion);
                miPrimerSocket.Listen(1);

                Console.WriteLine("Escuchando...");
                while (true)
                {
                    Socket Escuchar = miPrimerSocket.Accept();
                    Console.WriteLine("Conectado con exito");

                    ByRec = new byte[255];
                    int a = Escuchar.Receive(ByRec, 0, ByRec.Length, 0);
                    Array.Resize(ref ByRec, a);
                    Console.WriteLine("Cliente dice: " + Encoding.Default.GetString(ByRec)); //mostramos lo recibido
                }

            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error.ToString());
            }
            finally
            {
                miPrimerSocket.Close();
            }
            Console.WriteLine("Presione cualquier tecla para terminar");
            Console.ReadLine();
        }
    }
}
