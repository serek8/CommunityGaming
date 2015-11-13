using UnityEngine;
using System.Collections;

namespace CommunityGaming {

    public class PlayerInfo {

        public int healthPoints;
        public int killStreak;

        public PlayerInfo() {
            ResetStats();
        }
        
        public void ResetStats() {
            healthPoints = 5;
            killStreak = 0;
        }
    }

}

