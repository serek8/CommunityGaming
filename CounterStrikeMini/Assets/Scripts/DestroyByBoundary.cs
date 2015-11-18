using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("LEAVING...");
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
        }
       
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("ENTERING");
    }
    //void OnTriggerStay2d(Collider2D other)
    //{
    //    Debug.Log("Jestem w srodku");
    //}
}
