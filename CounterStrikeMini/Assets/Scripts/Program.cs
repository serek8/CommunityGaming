using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace CommunityGaming
{
    class Program : MonoBehaviour
    {
        WarriorGame wariorGame;
        private int numberOfPlayers=0;
       
        //public GameObject testCube;
        //public static Loom current;
        //public static UnityThreading.Dispatcher disp;


        void Start()
        {
            //Dispatcher mainDispatcher = Dispatcher.CurrentDispatcher;
            //disp =  UnityThreadHelper.Dispatcher;
            var tmp = Loom.Current;
            wariorGame = new WarriorGame();
            Thread thread = new Thread(new ThreadStart(() => wariorGame.startServer()));
            thread.Start();
            
          
        }
    
        public void StopServer()
        {
            wariorGame.Close();
        }
        
    }
}
