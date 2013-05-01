using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkCommsDotNet;
using System.Net;

namespace ServerApplication
{
    class Program
    {   
        public static string ConnectionFilePath;
        public static string MessagesFilePath;
        public static HashSet<string> ConnectionsList = new HashSet<string>();
        

        public static void Main(string[] args)
        {
            // incoming Connection List...  
            ConnectionFilePath = @"c:\temp\JTConnectionList.txt";
            MessagesFilePath = @"c:\temp\JTMessagesList.txt";   

            // Precent unhandled Exceptions from rogue packet type receives...
            NetworkComms.IgnoreUnknownPacketTypes = true;
            
            // Trigger the methods when packets are received from Clients...
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Connection", AddToConnectionList);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Disconnection", RemoveFromConnectionList);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Message", PrintIncomingMessage);

            /* Start listening for incoming connections using Oiginal method for all IP's and random port number between 10000-65535...
            TCPConnection.StartListening(true);
            ...*/

            // Specify exactly the IP and Port to listen on...                     
            IPEndPoint MyipLocalEndPoint = new IPEndPoint(IPAddress.Parse("192.168.1.11"), 11000);

            /* Prompt for IP and Port...
            Console.WriteLine("Enter the Server IP:port to listen on:");
            string serverInfo = Console.ReadLine();
            string serverIP = serverInfo.Split(':').First();
            int serverPort = int.Parse(serverInfo.Split(':').Last());
            IPEndPoint MyipLocalEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);*/

            // Make the connection...
            TCPConnection.StartListening(MyipLocalEndPoint);


            // Print out the IPs and ports we are now listening on...
            Console.WriteLine("Listening on:");
            foreach (System.Net.IPEndPoint localEndPoint in TCPConnection.ExistingLocalListenEndPoints()) Console.WriteLine("{0}:{1}", localEndPoint.Address, localEndPoint.Port);

            // Let the user close the server...
            Console.WriteLine("\nPress any key to close server.");
            Console.ReadKey(true);

            // We have used NetworkComms so we should ensure that we correctly call shutdown...
            NetworkComms.Shutdown();            
        }

        // Receive incoming initial Connection messages from Clients...
        public static void AddToConnectionList(PacketHeader header, Connection connection, string Connection)
        {
            /* Old method to create files on hard disk for testing the code...
             * 
               if (!File.Exists(ConnectionFilePath))
            {
                // Create a file to write to. 
                using (StreamWriter sw = File.CreateText(ConnectionFilePath))
                {

                }
            }
             
            using (StreamWriter sw = File.AppendText(ConnectionFilePath))
            {
                sw.WriteLine(connection.ConnectionInfo.RemoteEndPoint.Address.ToString());
            }  
             * 
             ...*/
            
            // Add incoming IP Address to HashSet list...            
            ConnectionsList.Add(connection.ConnectionInfo.RemoteEndPoint.Address.ToString());                                

            // Respond to sender that they are succesfully connected.            
            connection.SendObject("Connected", "CONNECTED!");
            Console.WriteLine(connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + " has CONNECTED!");

            // Display the Hashset List...
            foreach (var item in ConnectionsList) Console.WriteLine("Current Clients:\n" + item);
        }

        // When client closes, this Server should receive packet of string type "Disconnection", report to console and remove IP from ConnectionsList...
        public static void RemoveFromConnectionList(PacketHeader header, Connection connection, string Disconnection)
        {
            Console.WriteLine(connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + " has DISCONNECTED!");
            ConnectionsList.Remove(connection.ConnectionInfo.RemoteEndPoint.Address.ToString());
            if (ConnectionsList.Count > 0)
            {
                foreach (var item in ConnectionsList) Console.WriteLine("Current Clients:" + item);
            }
            else
            {
                Console.WriteLine("No Connected Clients.");
            }
            
        }        

        // Receive incoming messages to trigger Jungle Timers...
        public static void PrintIncomingMessage(PacketHeader header, Connection connection, string message)
        {            
            /* Test code to write messages to text file...
            if (!File.Exists(MessagesFilePath))
            {
                // Create a file to write to. 
                using (StreamWriter sw = File.CreateText(MessagesFilePath))
                {
                    
                }
            }

            // Add Message to Messages File...
            using (StreamWriter sw = File.AppendText(MessagesFilePath))
            {
                sw.WriteLine(message);
            }
            ...*/

            Console.WriteLine("\nRecieved " + "'" + message + "' from " + connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + ".");

            // Begin testing code for sendback to all clients...
            
            foreach(var item in NetworkComms.GetExistingConnection())
                item.SendObject("TimerControl", message);                              
            
        }
    }
}