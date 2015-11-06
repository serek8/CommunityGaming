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
        public GameObject testCube;


        void Start()
        {

            wariorGame = new WarriorGame();
            Thread thread = new Thread(new ThreadStart(() => wariorGame.startServer()));
            thread.Start();
        }
        void Update()
        {
            if(this.wariorGame.communicationUnits.Count()>numberOfPlayers)
            {
                //GameObject player = Instantiate(testCube, new Vector3( testCube.transform.position.x + UnityEngine.Random.Range(5.0f, 7.0f), testCube.transform.position.y + UnityEngine.Random.Range(3.0f, 5.0f), 0), Quaternion.identity) as GameObject;
                //player.name = ""+numberOfPlayers++;

            }
        }
    }
}
