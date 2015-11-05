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
                server = new TcpListener(IPAddress.Any,port);

                server.Start();
                Console.WriteLine("Server is running");
                while (true)
                {
                    Console.WriteLine("Waiting for a connection... ");
                    ClientObject client = new ClientObject( server.AcceptTcpClient());
                    clientList.Add(client);
                    T communicationUnit = this.createNewCommunicationUnit(client.client.GetStream());
                    communicationUnits.Add(communicationUnit);
                    Console.WriteLine("Connected! id:" + (client.id));
                    Thread thread = new Thread(new ThreadStart(() => handleClient(communicationUnit, client)));
                    thread.Start();
                    // client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
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
        
        public void Close()
        {
            this.server.Stop();
            Console.WriteLine("Server has been switched off");
        }
    }
}
