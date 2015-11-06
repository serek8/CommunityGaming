using System.Net.Sockets;
using UnityEngine;

namespace CommunityGaming
{
    public interface IBaseSerializer
    {
        void readFromStream();
        void writeFromStream();
    }
    
    public interface ISocketAccessibility
    {
        void didDisconnect();
    }
    
    
   public class CommunicationUnit :  IBaseSerializer, ISocketAccessibility
    {   
        public bool isConnected;
        
        protected InputSerializer inputSerializer;
        public CommunicationUnit(NetworkStream stream)
        {
            this.inputSerializer = new InputSerializer(stream);
            this.isConnected = true;
            
        }
        
        public void didDisconnect()
        {
            this.isConnected = false;
        }

        public virtual void readFromStream()  { }
        public virtual void writeFromStream() { }

       


    }
}
