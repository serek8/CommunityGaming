using UnityEngine;
using System.Collections;


namespace CommunityGaming {

    public class BulletMover : MonoBehaviour {

        public float speed;

        void Start() {
            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.velocity = transform.rotation*Vector3.up * speed; ;
        }
    }

}

