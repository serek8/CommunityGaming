using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace CommunityGaming
{
    class ClientObject
    {
        public int id;
        public TcpClient client;
        private static int globalId = 0;
        public ClientObject(TcpClient client)
        {
            this.id = globalId++;
            this.client = client;
        }
    }

    public class Game<T> where T : IBaseSerializer
    {
        private List<T> communicationUnits;// = new List<T>();
        private List<ClientObject> clientList;
        //CommunicationUnitFactory communicationUnitFactory;

        private T createNewCommunicationUnit(NetworkStream stream)
        {
            //return new T(stream);
            return (T)Activator.CreateInstance(typeof(T), stream);
        }

        //public abstract ComunicationUnit
        
        public Game()
        {
            this.communicationUnits = new List<T>();
            this.clientList = new List<ClientObject>();
            
            
            TcpListener server = null;
            try
            {
                Int32 port = 5555;
                server = new TcpListener(IPAddress.Any,port);

                server.Start();

                //this.setTimer();
                while (true)
                {
                    Console.WriteLine("Waiting for a connection... ");
                    ClientObject client = new ClientObject( server.AcceptTcpClient());
                    clientList.Add(client);
                    T communicationUnit = this.createNewCommunicationUnit(client.client.GetStream());
                    communicationUnits.Add(communicationUnit);
                    Console.WriteLine("Connected! id:" + (client.id));
                    Thread thread = new Thread(new ThreadStart(() => handleClient(communicationUnit)));
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

        private void handleClient(T communicationUnit)
        {  
            while (true)
            {
                communicationUnit.readFromStream();
            }
        }

        //  void setTimer()
        //  {
        //      System.Timers.Timer aTimer = new System.Timers.Timer();
        //      aTimer.Elapsed+=new ElapsedEventHandler(OnTimedEvent);
        //      aTimer.Interval=5000;
        //      aTimer.Enabled=true;
        //  }
        
        //  private static void OnTimedEvent(object source, ElapsedEventArgs e)
        //  {
        //    Console.WriteLine("Printing all CommunityUnit values:");
        //    for(int i=0 ; i<source.communicationUnits.Count() ; i++)
        //    {
        //        Console.WriteLine("Client:{0}\ta:{1}",i, communicationUnits[i].a);
        //    }
        //  }

    }
}
