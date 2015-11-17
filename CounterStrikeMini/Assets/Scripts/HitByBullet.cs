using UnityEngine;
using System.Collections;

public class HitByBullet : MonoBehaviour {
    
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            
            Destroy(other.gameObject);
            //this.gameObject.GetComponent<UnityCommunicationUnit>().PlayerGotHit();
            Debug.Log(this.gameObject);
            //Debug.Log(this.gameObject.GetComponent<UnityCommunicationUnit>());
            //Debug.Log(this.gameObject.transform.parent.gameObject.GetComponent<UnityCommunicationUnit>());
            this.gameObject.transform.parent.gameObject.GetComponent<UnityCommunicationUnit>().PlayerGotHit();
        }
        //if()
    }
}
