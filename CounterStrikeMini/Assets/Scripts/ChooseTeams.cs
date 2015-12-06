using UnityEngine;
using System.Collections;
using CommunityGaming;

public class ChooseTeams : MonoBehaviour {

    public BoxCollider2D team;
    private TeamInfo left;
    private TeamInfo right;

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag=="PlayerInstance")
        {
            //Debug.Log("PLAYER JEST  NA DRUZYNIE" + this.gameObject.name);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerInstance")
        {
            other.gameObject.GetComponent<PlayerInfo>().setTeam(this.gameObject.name[0]);
            Debug.Log(other.gameObject.GetComponent<PlayerInfo>().getTeam());
        }
    }
   

   

  
}
