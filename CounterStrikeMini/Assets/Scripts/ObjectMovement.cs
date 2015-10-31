using UnityEngine;
using System.Collections;

public class ObjectMovement : MonoBehaviour {
    float speed = 20.0f;

    void FixedUpdate()
    {
       
       
		if (Input.GetKey(KeyCode.UpArrow))
        {
            var move = new Vector3(0f, 0.5f, 0f);
            transform.position += move * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {

            var move = new Vector3(0f, -0.5f, 0f);
            transform.position += move * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {

            var move = new Vector3(-0.5f, 0f, 0f);
            transform.position += move * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {

            var move = new Vector3(0.5f, 0f, 0f);
            transform.position += move * speed * Time.deltaTime;
        }
    }
}
