using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CommunityGaming
{
	class WarriorCommunicationUnit : CommunicationUnit
	{
        // Tylko tego uzywasz        
        public int movement, rotation, action;
                
        
        // Core part        
        public WarriorCommunicationUnit(NetworkStream stream)
        :base(stream)
        {}              
	public override void readFromStream() 
        {
            this.movement = inputSerializer.readIntFromStream();
            this.rotation = inputSerializer.readIntFromStream();
            this.action = inputSerializer.readIntFromStream();
            Console.WriteLine("movement:{0}, rotation:{1}, action{2}", movement, rotation, action);
         }
        public override void writeFromStream() { }	
	}		
}