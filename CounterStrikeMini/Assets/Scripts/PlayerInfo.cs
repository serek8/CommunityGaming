using UnityEngine;
using System.Collections;

namespace CommunityGaming {

    public class PlayerInfo : MonoBehaviour {

        public int healthPoints;
        public int killStreak;

        void Start()
        {   
            ResetStats();
        }
        
        public void ResetStats() {
            healthPoints = 5;
            killStreak = 0;
        }
    }

}

