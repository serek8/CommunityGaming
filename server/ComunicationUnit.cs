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
    }
    
    
    class CommunicationUnit : IBaseSerializer
    {
        //new class
        
        protected InputSerializer inputSerializer;
        
        //Game game;
        //private NetworkStream stream;

        public CommunicationUnit(NetworkStream stream)
        {
            this.inputSerializer = new InputSerializer(stream);
            //this.stream = stream;
            //this.game = game;
        }


        public virtual void readFromStream()  { }
        public virtual void writeFromStream() { }

       


    }
}
