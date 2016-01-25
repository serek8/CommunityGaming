using System;
using System.Net.Sockets;
using UnityEngine;

namespace CommunityGaming
{
    public class WarriorCommunicationUnit : CommunicationUnit, ISocketAccessibility
	{
       
        // Tylko tego uzywasz        
        public int movement, rotation, action, speed;
        //public GameObject players;
        public bool isActionSet = false;
        //public delegate void NextPrimeDelegate();
        private UnityCommunicationUnit ucu;
        GameObject player;


        // Core part        
        public WarriorCommunicationUnit(NetworkStream stream)
        : base(stream)
        {
            movement = -1; // -1 is default
            rotation = -1;
            action = -1;
            speed = -1;

            /* Program.disp.Dispatch(() => */


            Loom.QueueOnMainThread(() =>
                 {
                     //This is working for sure
                     //ucu = GameObject.FindGameObjectWithTag("Player").AddComponent<UnityCommunicationUnit>();
                     //ucu.setWarrior(this);

                      player = new GameObject();
                     player.GetComponent<Transform>().SetParent(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>());
                     ucu= player.AddComponent<UnityCommunicationUnit>();
                     ucu.setWarrior(this);
                    
                     
                 });
           

        }              
	public override void readFromStream() 
        {
            lock (this)
            {
             this.movement = inputSerializer.readIntFromStream();
             this.rotation = inputSerializer.readIntFromStream();
             this.action = inputSerializer.readIntFromStream();
               // this.speed = inputSerializer.readIntFromStream();
            }
             if(this.action==1) isActionSet=true;
            Console.WriteLine("movement:{0}, speed:{1}, rotation:{2}, action{3}", movement, speed, rotation, action);
        }
        public override void writeFromStream() { }	
		
		public override void didDisconnect()
        {

            Loom.QueueOnMainThread(() =>
            {
                 GameObject.Destroy(player);


            });
            
        }
		
		public override void AdoptNewData(byte[] bytes)
        {
             this.movement = BitConverter.ToInt32(bytes, 0);
             this.rotation = BitConverter.ToInt32(bytes, 4);
             this.action   = BitConverter.ToInt32(bytes, 8);
             if(this.action==1) isActionSet=true;
              Console.WriteLine("movement:{0} rotation:{1}, action{2}", movement, rotation, action);   
        }
	}		
   
}