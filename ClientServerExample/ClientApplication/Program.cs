using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkCommsDotNet;

namespace ClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Message", PrintIncomingMessage);

            //Request server IP and port number
            Console.WriteLine("Please enter the server IP and port in the format 192.168.0.1:10000 and press return:");
            string serverInfo = Console.ReadLine();

            //Parse the necessary information out of the provided string
            string serverIP = serverInfo.Split(':').First();
            int serverPort = int.Parse(serverInfo.Split(':').Last());


            ConsoleKeyInfo key;
            while (true)
            {
                var line = Console.ReadLine();
                if (line.ToLower() == "quit")
                    break;

                //Write some information to the console window
                string messageToSend = line;
                Console.WriteLine("Sending message to server saying '" + messageToSend + "'");

                //Send the message in a single line
                NetworkComms.SendObject("Message", serverIP, serverPort, messageToSend);

                //Check if user wants to go around the loop
                Console.WriteLine();
                Console.WriteLine("\nPress q to quit or any other key to send another message.");
                
 
            }

            //We have used comms so we make sure to call shutdown
            NetworkComms.Shutdown();
        }

        private static void PrintIncomingMessage(PacketHeader packetHeader, Connection connection, string incomingObject)
        {
            Console.WriteLine("Server got our message:" + incomingObject);
        }
    }
}