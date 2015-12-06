using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CommunityGaming
{
    public interface IBaseSerializer
    {
        void readFromStream();
        void writeFromStream();
        void AdoptNewData(byte[] bytes);
    }
    
    public interface ISocketAccessibility
    {
        void didDisconnect();
    }
    
    
    class CommunicationUnit : IBaseSerializer, ISocketAccessibility
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
        public virtual void AdoptNewData(byte[] bytes) {}
 
       


    }
}
