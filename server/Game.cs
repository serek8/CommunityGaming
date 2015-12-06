using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;
using System.IO;

namespace CommunityGaming
{
    class ClientObject
    {
        public int id;
        public TcpClient client;
        public bool isConnected;
        private static int globalId = 0;
        public ClientObject(TcpClient client)
        {
            this.id = globalId++;
            this.client = client;
            this.isConnected = true;
        }
    }

    public class Game<T> where T : IBaseSerializer, ISocketAccessibility
    {
        public List<T> communicationUnits;
        private List<ClientObject> clientList;
        private  TcpListener server;
        public UdpClient udpServer;

        ~Game()
        {
            this.Close();
        }

        private T createNewCommunicationUnit(NetworkStream stream)
        {
            return (T)Activator.CreateInstance(typeof(T), stream);
        }

        public Game()
        {
            this.communicationUnits = new List<T>();
            this.clientList = new List<ClientObject>();
            this.server = null;
        }
        
        public void startServer()
        {
            try
            {
                Int32 port = 5555;
                // Setting TCP server
                server = new TcpListener(IPAddress.Any,port);
                server.Start();
                Console.WriteLine("TCP - Running");
                
                //Setting UDP server
                this.udpServer = new UdpClient(5557);
                Console.WriteLine("UDP - Running");
                Thread udpServerThread = new Thread(new ThreadStart(() => handleUdpServer()));
                    udpServerThread.Start();
                
                Console.WriteLine("Server is ready");
                while (true)
                {
                    Console.WriteLine("Waiting for a connection... ");
                    ClientObject client = new ClientObject( server.AcceptTcpClient());
                    clientList.Add(client);
                    T communicationUnit = this.createNewCommunicationUnit(client.client.GetStream());
                    communicationUnits.Add(communicationUnit);
                    Console.WriteLine("Connected! id:" + (client.id));
                    // Thread thread = new Thread(new ThreadStart(() => handleClient(communicationUnit, client)));
                    // thread.Start();
                    // client.Close();
                }
            }
            catch (SocketException)
            {
                Console.WriteLine("Error -> Cannot establish connection. \nPlease check if there is other application which uses port 5555");
                //Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
        }

        private void handleClient(T communicationUnit, ClientObject clientObject)
        {  
            try
            {
                while(true)
                communicationUnit.readFromStream();
            }
            catch(IOException)
            {
                Console.WriteLine("Client {0} has left the Game.", clientObject.id);
                this.communicationUnits[clientObject.id].didDisconnect();
                clientObject.isConnected = false;
                clientObject.client.Close();
            }
        }
        
        private void handleUdpServer()
        {
            var groupEP = new IPEndPoint(IPAddress.Any, 0);
            while(true)
            {
                //Console.WriteLine("Waiting for UDP message...");
 			    byte[] bytes = udpServer.Receive(ref groupEP);    
                //Console.WriteLine("This is the message you received from {0}", groupEP.Address.ToString());
                foreach (ClientObject clientObject in this.clientList)
                {
                    if(clientObject.isConnected==false) continue;
                    IPAddress clientObjectAdress = IPAddress.Parse(((IPEndPoint)clientObject.client.Client.RemoteEndPoint).Address.ToString());
                    IPAddress udpClientAdress = groupEP.Address;
                    if(clientObjectAdress.Equals(udpClientAdress))
                    {
                        this.communicationUnits[clientObject.id].AdoptNewData(bytes);
                        break;
                    }
                }
            }
        }
        
        
        public void Close()
        {
            this.server.Stop();
            Console.WriteLine("Server has been switched off");
        }
    }
}
