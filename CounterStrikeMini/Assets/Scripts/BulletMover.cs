using UnityEngine;
using System.Collections;

public class BulletMover : MonoBehaviour {


    public float speed;

	void Start () {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = transform.forward * speed;
	}
	

}
