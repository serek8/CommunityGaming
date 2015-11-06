using System;
using System.Net.Sockets;
using UnityEngine;

namespace CommunityGaming
{
    public class WarriorCommunicationUnit : CommunicationUnit, ISocketAccessibility
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

            UnityCommunicationUnit ucu = GameObject.Find("Players").AddComponent<UnityCommunicationUnit>();
            ucu.setWarrior(this);

        }              
	public override void readFromStream() 
        {
            lock (this)
            {
             this.movement = inputSerializer.readIntFromStream();
             this.rotation = inputSerializer.readIntFromStream();
             this.action = inputSerializer.readIntFromStream();
            }
             if(this.action==1) isActionSet=true;
             Console.WriteLine("movement:{0}, rotation:{1}, action{2}", movement, rotation, action);
        }
        public override void writeFromStream() { }	
	}		
   
}