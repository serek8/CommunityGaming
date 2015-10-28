using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CommunityGaming
{
    public class InputSerializer
    {
        private NetworkStream networkStream;
        
        public InputSerializer(NetworkStream networkStream)
        {
            this.networkStream = networkStream;
        }
        
        public int readIntFromStream()
        {
            Byte[] bytes = new Byte[4];
            int i = 0;
            int totalBytes = 0;
            while (totalBytes < 3)
            {

                i=networkStream.Read(bytes, totalBytes, bytes.Length-totalBytes);
                totalBytes += i;
                Console.WriteLine("Reeceived {0}bytes:",i);
                // Translate data bytes to a ASCII string.
            
            }
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}