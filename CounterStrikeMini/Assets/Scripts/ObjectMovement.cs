using UnityEngine;
using System.Collections;

public class ObjectMovement : MonoBehaviour {
    float speed = 20.0f;
    bool moved = false;
    void moveUp()
    {
        var move = new Vector3(0f, 0.5f, 0f);
        transform.position += move * speed * Time.deltaTime;
        moved = true;
    }
    void moveDown()
    {
        var move = new Vector3(0f, -0.5f, 0f);
        transform.position += move * speed * Time.deltaTime;
    }
    void moveLeft()
    {

        var move = new Vector3(-0.5f, 0f, 0f);
        transform.position += move * speed * Time.deltaTime;
    }
    void moveRight()
    {
        var move = new Vector3(0.5f, 0f, 0f);
        transform.position += move * speed * Time.deltaTime;
    }
   
    }

