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
        // Old Test code for writing connections and messages to text file on hdd...
        public static string ConnectionFilePath;
        public static string MessagesFilePath;
        
        // Create Connectionlist HashSet...
        public static HashSet<string> ConnectionsList = new HashSet<string>();
        
        // Set Latest available version of Client Software...
        public static string LatestClientVersion = "1.5a";        
        
        public static void Main(string[] args)
        {
            // Old Method for incoming Connection List...  
            // ConnectionFilePath = @"c:\temp\JTConnectionList.txt";
            // MessagesFilePath = @"c:\temp\JTMessagesList.txt";

            // Prevent unhandled Exceptions from rogue packet type receives...
            NetworkComms.IgnoreUnknownPacketTypes = true;
            
            // Trigger the methods when packets are received from Clients...
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Connection", AddToConnectionList);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Disconnection", RemoveFromConnectionList);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Message", PrintIncomingMessage);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("version", VersionCheck);

            /* Start listening for incoming connections using Oiginal method for all IP's and random port number between 10000-65535...
            TCPConnection.StartListening(true);
            ...*/

            // Or specify exactly the IP and Port to listen on...                     
            // From Home...
            //IPEndPoint MyipLocalEndPoint = new IPEndPoint(IPAddress.Parse("192.168.1.11"), 11000);
            // ...Or From Work...
            // IPEndPoint MyipLocalEndPoint = new IPEndPoint(IPAddress.Parse("172.16.69.69"), 11000);

            // Prompt for IP and Port...
            Console.WriteLine("Enter IP Adddress:port to listen on: (example: 192.168.1.11:11000):");
            var serverInfo = Console.ReadLine();
            if (serverInfo != null)
            {
                var serverIp = serverInfo.Split(':').First();
                var serverPort = int.Parse(serverInfo.Split(':').Last());
                var myipLocalEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);

                // Make the connection...
                TCPConnection.StartListening(myipLocalEndPoint);
            }


            // Print out the IPs and ports we are now listening on...
            Console.WriteLine("Server version " + LatestClientVersion + " Listening on:");            
            foreach (var localEndPoint in TCPConnection.ExistingLocalListenEndPoints()) Console.WriteLine("{0}:{1}", localEndPoint.Address, localEndPoint.Port);

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
        }

        // When client closes, this Server should receive packet of string type "Disconnection", report to console and remove IP from ConnectionsList...
        public static void RemoveFromConnectionList(PacketHeader header, Connection connection, string Disconnection)
        {
            Console.WriteLine(connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + " has DISCONNECTED!");
            ConnectionsList.Remove(connection.ConnectionInfo.RemoteEndPoint.Address.ToString());            
            if (ConnectionsList.Count > 0)
            {
                Console.WriteLine("Current Clients:");
                foreach (var item in ConnectionsList) Console.WriteLine(item);
            }
            else
            {
                Console.WriteLine("No Connected Clients.");
            }
            
        }
        
        // Check Client version and send update signal if needed.
        public static void VersionCheck(PacketHeader header, Connection connection, string message)
        {            
            if (message == LatestClientVersion)
            {
                Console.WriteLine(connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + " is up to date.");                
                // Display all connected Clients...
                foreach (var item in ConnectionsList) Console.WriteLine("Current Clients:\n" + item);
            }
            else
            {
                Console.WriteLine(connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + " is version " + message + ", and has been notified of available update.");
                connection.SendObject("version", LatestClientVersion);
                // Display all connected Clients...
                Console.WriteLine("Current Clients:");
                foreach (var item in ConnectionsList) Console.WriteLine(item);
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

            // TimerControl sendback to all clients...
            
            foreach(var item in NetworkComms.GetExistingConnection())
                item.SendObject("TimerControl", message);                              
            
        }
    }
}