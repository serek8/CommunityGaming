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
        public int movement, rotation, action;
        public bool isActionSet = false;
        
        // Core part        
        public WarriorCommunicationUnit(NetworkStream stream)
        :base(stream)
        {
             movement = -1; // -1 is default
             rotation = -1;
             action   = -1;   
                
        }              
	public override void readFromStream() 
        {
        //     lock (this)
        //     {
        //      this.movement = inputSerializer.readIntFromStream();
        //      this.rotation = inputSerializer.readIntFromStream();
        //      this.action   = inputSerializer.readIntFromStream();
        //     }
        //      if(this.action==1) isActionSet=true;
        //      Console.WriteLine("movement:{0} rotation:{1}, action{2}", movement, rotation, action);
                Console.WriteLine("Warning someone uses old version of Core");
        }
        public override void writeFromStream() { }	
        
        
        
        public override void AdoptNewData(byte[] bytes)
        {
             this.movement = BitConverter.ToInt32(bytes, 0);
             this.rotation = BitConverter.ToInt32(bytes, 4);
             this.action   = BitConverter.ToInt32(bytes, 8);
              Console.WriteLine("movement:{0} rotation:{1}, action{2}", movement, rotation, action);   
        }
        
	}		
}