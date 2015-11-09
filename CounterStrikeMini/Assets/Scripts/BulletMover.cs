using UnityEngine;
using System.Collections;

namespace CommunityGaming {

    public class BulletMover : MonoBehaviour {

        public float speed;

        void Start() {
            Rigidbody rigidBody = GetComponent<Rigidbody>();
            rigidBody.velocity = transform.forward * speed;
        }


    }

}

