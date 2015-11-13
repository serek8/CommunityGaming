using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CommunityGaming
{
	class WarriorCommunicationUnit : CommunicationUnit, ISocketAccessibility
	{
        // Tylko tego uzywasz        
        public int movement, speed, rotation, action;
        public bool isActionSet = false;
        
        // Core part        
        public WarriorCommunicationUnit(NetworkStream stream)
        :base(stream)
        {
             movement = -1; // -1 is default
             speed    = -1;
             rotation = -1;
             action   = -1;   
                
        }              
	public override void readFromStream() 
        {
            lock (this)
            {
             this.movement = inputSerializer.readIntFromStream();
             this.speed    = inputSerializer.readIntFromStream();
             this.rotation = inputSerializer.readIntFromStream();
             this.action   = inputSerializer.readIntFromStream();
            }
             if(this.action==1) isActionSet=true;
             Console.WriteLine("movement:{0}, speed:{1}, rotation:{2}, action{3}", movement, speed, rotation, action);
        }
        public override void writeFromStream() { }	
	}		
}