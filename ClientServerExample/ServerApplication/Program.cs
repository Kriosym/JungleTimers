using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using NetworkCommsDotNet;
using System.Net;

namespace ServerApplication
{
    class Program
    {   
        /* Old Test code for writing connections and messages to text file on hdd...
        public static string ConnectionFilePath;
        public static string MessagesFilePath; */           

        // Create Connectionlist HashSet...
        public static HashSet<string> ConnectionsList = new HashSet<string>();

        // Track last client to disconnect for notification to other clients...
        public static string disconLastclient;

        // Definable TCP port number to listen on...
        public static int definedPort = 11000;
        
        // Set Latest available version of Client Software...
        public static string LatestClientVersion = "1.5e";


        private static void ServerInfo()
        {
            Console.WriteLine("\n\n~ Jungle Timers by Kriosym version " + LatestClientVersion + " ~");
            Console.WriteLine("\nMENU\n======================================");
            Console.WriteLine("x - Close Connections and Exit");
            Console.WriteLine("i - Re-Display Menu");
            Console.WriteLine("======================================");
            Console.Write("Enter Selection: ");
            return;
        }

        public static void Main(string[] args)
        {
            Console.Title = "Jungle Timers Server by Kriosym (enter i to Re-Display Menu)";
            ConsoleKeyInfo cki;
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
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

            /* Oiginal method 1 to Start listening for incoming connections using all IP's and random port number between 10000-65535...
            TCPConnection.StartListening(true);
            ...

            ...Or 2 to specify exactly the IP and Port to listen on...                     
            From Home...
            IPEndPoint MyipLocalEndPoint = new IPEndPoint(IPAddress.Parse("192.168.1.136"), 11000);
            
            ...or 3 to prompt for IP and Port...
            Console.WriteLine("Enter IP Adddress:port to listen on: (example: 192.168.1.11:11000):");
            var serverInfo = Console.ReadLine();
            if (serverInfo != null)
            {
                var serverIp = serverInfo.Split(':').First();
                var serverPort = int.Parse(serverInfo.Split(':').Last());
                var myipLocalEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);

                // Make the connection...
                TCPConnection.StartListening(myipLocalEndPoint);
            } */


            // BEST METHOD 4 - Listen on all IPv4 Addresses, user defined port.
            Console.WriteLine("~ Jungle Timers by Kriosym ~");
            Console.WriteLine("Server version " + LatestClientVersion + " listening on: ");
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                    IPEndPoint myipLocalEndPoint = new IPEndPoint(IPAddress.Parse(localIP), definedPort);
                    TCPConnection.StartListening(myipLocalEndPoint);
                    Console.Write(localIP + " | ");
                }
            }
            Console.Write("TCP port " + definedPort);
            Console.Title = "Jungle Timers Server listening on " + localIP + ":" + definedPort + " | (enter i to Re-Display Menu)";

            
            // Options Menu...
            Console.TreatControlCAsInput = true;            
            Console.WriteLine("\n\nMENU\n======================================");
            Console.WriteLine("x - Close Connections and Exit");
            Console.WriteLine("i - Re-Display Menu");
            Console.WriteLine("======================================");
            Console.Write("Enter Selection: ");

            bool exitnow = false;
            do
            {
                cki = Console.ReadKey();
                string choice = cki.Key.ToString();                
                try
                {

                }
                catch (Exception)
                {
                    continue;
                }
                switch (choice)
                {
                    case "X":
                        exitnow = true;
                        Console.WriteLine("\n\nGoodbye!");
                        NetworkComms.Shutdown();
                        Environment.Exit(-1);
                        break;
                    case "I":
                        ServerInfo();
                        continue;
                }
            } while (!exitnow);
        }

        // Receive incoming initial Connection messages from Clients...
        public static void AddToConnectionList(PacketHeader header, Connection connection, string Connection)
        {
            // Add incoming IP Address to HashSet list...            
            ConnectionsList.Add(connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + "|" + Connection);                                

            // Respond to sender that they are succesfully connected.            
            connection.SendObject("Connected", "CONNECTED!");
            Console.WriteLine("\n" + connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + " has CONNECTED!");
                        
            // Send all active connections to all active connections for Client List.
            foreach (var item in NetworkComms.GetExistingConnection())
            {
                foreach (var items in ConnectionsList.Distinct())
                {
                    item.SendObject("Connection", items);
                }
            }

            /* Old method to create files on hard disk for testing the code...              
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
            } */
        }

        // When client closes, this Server should receive packet of string type "Disconnection", report to console and remove IP from ConnectionsList...
        public static void RemoveFromConnectionList(PacketHeader header, Connection connection, string Disconnection)
        {
            // Formulate which client has just disconnected...
            disconLastclient = connection.ConnectionInfo.RemoteEndPoint.Address.ToString();
            string disconClientName = ConnectionsList.Single(element => element.Contains(disconLastclient));                       
            Console.WriteLine("\n" + disconClientName + " has DISCONNECTED!");

            // ...and remove it from ConnectionsList HashSet
            ConnectionsList.RemoveWhere(element => element.Contains(disconLastclient));

            // Close the connection...                                            
            connection.CloseConnection(false);    

            // Notify remaining clients of the disconnection...
            foreach (var item in NetworkComms.GetExistingConnection())
            {
                item.SendObject("Disconnection", disconLastclient);
            }

            if (ConnectionsList.Count > 0)
            {
                Console.WriteLine("\nCurrent Clients:");
                foreach (var item in ConnectionsList) { Console.WriteLine(item); };
                
                var currentclients = ConnectionsList.Distinct();    
                foreach (var item in NetworkComms.GetExistingConnection())
                {
                    item.SendObject("Connection", currentclients);
                }
            }
            if (ConnectionsList.Count == 0)
            {
                Console.WriteLine("No Connected Clients.");
            }
            
        }
        
        // Check Client version and send update signal if needed.
        public static void VersionCheck(PacketHeader header, Connection connection, string version)
        {            
            if (version == LatestClientVersion)
            {
                Console.WriteLine(connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + " is up to date.");                
                // Display all connected Clients...
                Console.WriteLine("\nCurrent Clients:");
                foreach (var item in ConnectionsList) { Console.WriteLine(item); }
            }
            else
            {
                connection.SendObject("version", LatestClientVersion);
                Console.WriteLine(connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + " is version " + version + ", and has been notified of available update.");
                // Display all connected Clients...
                Console.WriteLine("\nCurrent Clients:");
                foreach (var item in ConnectionsList) { Console.WriteLine(item); }
            }           
        }

        // Receive incoming messages to trigger Jungle Timers...
        public static void PrintIncomingMessage(PacketHeader header, Connection connection, string message)
        {
            Console.WriteLine("\nRecieved " + "'" + message + "' from " + connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + ".");

            // TimerControl sendback to all clients...            
            foreach (var item in NetworkComms.GetExistingConnection())
            { item.SendObject("TimerControl", message); }

            /* Test code to write messages to text file...
            if (!File.Exists(MessagesFilePath))
            {
                // Create a file to write to. 
                using (StreamWriter sw = File.CreateText(MessagesFilePath)){}
            }
            // Add Message to Messages File...
            using (StreamWriter sw = File.AppendText(MessagesFilePath))
            {sw.WriteLine(message);} */
        }
    }
}