using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using NetworkCommsDotNet;
using System.Net;

namespace JungleTimers
{
    public partial class ServerApplicationForm3 : Form
    {
        public static HashSet<string> ConnectionsList_Server = new HashSet<string>();
        public static string disconLastclient;
        public static int definedPort = 11000;
        public static string LatestClientVersion = "1.6b";
        
        public ServerApplicationForm3()
        {
            InitializeComponent();
        }

        private void ServerApplicationForm3_Load(object sender, EventArgs e)
        {
            richTextBox1.AppendText("\n~ Jungle Timers by Kriosym version " + LatestClientVersion + " ~");
        }

        private static void button1_start_Click(object sender, EventArgs e)
        {
            // Prevent unhandled Exceptions from rogue packet type receives...
            NetworkComms.IgnoreUnknownPacketTypes = true;

            // Trigger the methods when packets are received from Clients...
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Connection", AddToConnectionList_Server);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Disconnection", RemoveFromConnectionList_Server);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Message", PrintIncomingMessage);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("version", VersionCheck_Server);
            
            // Begin Listen on all IPv4 Addresses...
            IPHostEntry host;
            string localIP;
            host = Dns.GetHostEntry(Dns.GetHostName());


            richTextBox1.AppendText("Server version " + LatestClientVersion + " listening on: ");
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                    IPEndPoint myipLocalEndPoint = new IPEndPoint(IPAddress.Parse(localIP), definedPort);
                    TCPConnection.StartListening(myipLocalEndPoint);
                    richTextBox1.AppendText(localIP + " | ");
                }
            }
            richTextBox1.AppendText("TCP port " + definedPort + Environment.NewLine);
        }

        private static void AddToConnectionList_Server(PacketHeader header, Connection connection, string Connection)
        {
            // Add incoming IP Address to HashSet list...            
            ConnectionsList_Server.Add(connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + "|" + Connection);

            // Respond to sender that they are succesfully connected.            
            connection.SendObject("Connected", "CONNECTED!");
            //richTextBox1.AppendText("\n" + connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + " has CONNECTED!" + Environment.NewLine);

            // Send all active connections to all active connections for Client List.
            foreach (var item in NetworkComms.GetExistingConnection())
            {
                foreach (var items in ConnectionsList_Server.Distinct())
                {
                    item.SendObject("Connection", items);
                }
            }
        }

        // When client closes, this Server should receive packet of string type "Disconnection", report to console and remove IP from ConnectionsList...
        private static void RemoveFromConnectionList_Server(PacketHeader header, Connection connection, string Disconnection)
        {
            // Formulate which client has just disconnected...
            disconLastclient = connection.ConnectionInfo.RemoteEndPoint.Address.ToString();
            string disconClientName = ConnectionsList_Server.Single(element => element.Contains(disconLastclient));
            

            // ...and remove it from ConnectionsList HashSet
            ConnectionsList_Server.RemoveWhere(element => element.Contains(disconLastclient));

            // Close the connection...                                            
            connection.CloseConnection(false);

            // Notify remaining clients of the disconnection...
            foreach (var item in NetworkComms.GetExistingConnection())
            {
                item.SendObject("Disconnection", disconLastclient);
            }

            if (ConnectionsList_Server.Count > 0)
            {
                richTextBox1.AppendText("\nCurrent Clients:" + Environment.NewLine);
                foreach (var item in ConnectionsList_Server) { richTextBox1.AppendText(item); };

                var currentclients = ConnectionsList_Server.Distinct();
                foreach (var item in NetworkComms.GetExistingConnection())
                {
                    item.SendObject("Connection", currentclients);
                }
            }
            if (ConnectionsList_Server.Count == 0)
            {
                richTextBox1.AppendText("No Connected Clients." + Environment.NewLine);
            }

        }

        // Check Client version and send update signal if needed.
        private static void VersionCheck_Server(PacketHeader header, Connection connection, string version)
        {
            if (version == LatestClientVersion)
            {
                richTextBox1.AppendText(connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + " is up to date." + Environment.NewLine);
                // Display all connected Clients...
                richTextBox1.AppendText("\nCurrent Clients:" + Environment.NewLine);
                foreach (var item in ConnectionsList_Server) { richTextBox1.AppendText(item + Environment.NewLine); }
            }
            else
            {
                connection.SendObject("version", LatestClientVersion);
                richTextBox1.AppendText(connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + " is version " + version + ", and has been notified of available update." + Environment.NewLine);
                // Display all connected Clients...
                richTextBox1.AppendText("\nCurrent Clients:"  + Environment.NewLine);
                foreach (var item in ConnectionsList_Server) { richTextBox1.AppendText(item + Environment.NewLine); }
            }
        }

        // Receive incoming messages to trigger Jungle Timers...
        private static void PrintIncomingMessage(PacketHeader header, Connection connection, string message)
        {
            richTextBox1.AppendText("\nRecieved " + "'" + message + "' from " +
                              connection.ConnectionInfo.RemoteEndPoint.Address.ToString() + "." + Environment.NewLine);

            // TimerControl sendback to all clients...            
            foreach (var item in NetworkComms.GetExistingConnection())
            {
                item.SendObject("TimerControl", message);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
