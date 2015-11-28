using UnityEngine;
using System.Collections;

namespace CommunityGaming {

    public class IndestructibleObject : MonoBehaviour {

        void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Bullet") {
                Destroy(other.gameObject);
            }
        }
    }
}