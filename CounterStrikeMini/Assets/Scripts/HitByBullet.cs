﻿using UnityEngine;
using System.Collections;
using CommunityGaming;

public class HitByBullet : MonoBehaviour {
    
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            this.gameObject.transform.parent.gameObject.GetComponent<UnityCommunicationUnit>().PlayerGotHit(other.gameObject.GetComponent<BulletMover>().getWhoShot());

        }

        //if()
    }
}
