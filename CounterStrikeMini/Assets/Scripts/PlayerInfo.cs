using UnityEngine;
using System.Collections;

namespace CommunityGaming {

    public class PlayerInfo: MonoBehaviour{

        public int healthPoints;
        public int killStreak;
        int deathCounter;
        int killCounter;
        public char team {
            set { this.team = value; }
            get { return this.team; }
        }

        void Start() {
            ResetStats();
            deathCounter = 0;
            killCounter = 0;
        }
        //public PlayerInfo() {   
        //    ResetStats();
        //    deathCounter = 0;
        //    killCounter = 0;
        //}
        ////this constructor will be used by default for setting team
        //public PlayerInfo(char team) {
        //    ResetStats();
        //    deathCounter = 0;
        //    killCounter = 0;
        //    this.team = team;
        //}
        
        public void ResetStats() {
            healthPoints = 5;
            killStreak = 0;
        }

        public void incrementDeathCounter() {
            this.deathCounter++;
        }
        
        public int getDeathCounter() {
            return this.deathCounter;
        }

        public void incrementKillCounter() {
            this.killCounter++;
        }

        public int getKillCounter() {
            return this.killCounter;
        }


    }

}

