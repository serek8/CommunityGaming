using UnityEngine;
using System.Collections;
using CommunityGaming;

public class ChooseTeams : MonoBehaviour {

    public BoxCollider2D team;
    private TeamInfo left;
    private TeamInfo right;
    public Sprite teamA, teamB;
    

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
            SpriteRenderer child = other.gameObject.GetComponentsInChildren<SpriteRenderer>()[1];
            if (other.gameObject.GetComponent<PlayerInfo>().getTeam()=='a')
            {
              
                child.sprite = teamA; 
            }
            else
            {
                child.sprite = teamB;
            }
            
            Debug.Log(other.gameObject.GetComponent<PlayerInfo>().getTeam());
        }
    }
   

   

  
}
