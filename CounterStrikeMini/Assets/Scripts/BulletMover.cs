using UnityEngine;
using System.Collections;


namespace CommunityGaming {

    public class BulletMover : MonoBehaviour {

        public float speed;
        private Player whoShot;

        void Start() {
            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.velocity = transform.rotation*Vector3.up * speed; ;
        }

        public void setWhoShot(Player p)
        {
            whoShot = p;
        }

        public Player getWhoShot()
        {
            return whoShot;
        }
    }

}

