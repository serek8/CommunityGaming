using UnityEngine;
using System.Collections;
using CommunityGaming;
using System;
using System.Net.Sockets;

public class UnityCommunicationUnit : MonoBehaviour, ISocketAccessibility, IBaseSerializer {

    #region UnityCommunicationUnit variables
    public WarriorCommunicationUnit warrior;  
    private Player player;
    private bool teamsChosen = false;

    #endregion

    // Use this for initialization
    void Start () {
        player= gameObject.AddComponent<Player>();

        player.setUnityCommunicationUnit(this);
        player.InitializePlayer();
    }

    public void setWarrior(WarriorCommunicationUnit w)
    {
        warrior = w;     
    }

    // Update is called once per frame
    void Update()
    {
        //if (teamsChosen)
        //{
            if (warrior.movement != -1)
            {
                player.movePlayer(warrior);
            }
            if (warrior.rotation != -1)
            {
                player.RotateObject(warrior.rotation);
            }
            player.ShotBullet(warrior);
        
        //else
        //{
        //    player.ChooseTeams();
        //}
    }

    public void PlayerGotHit()
    {
        player.PlayerGotHit();
    }
      
    #region Serverside methods
    public void didDisconnect() {
        Destroy(player.gameObject);
       
    }

    public UnityCommunicationUnit(NetworkStream stream) {

    }

    public void readFromStream() {
        ((IBaseSerializer)warrior).readFromStream();
    }

    public void writeFromStream() {
        ((IBaseSerializer)warrior).writeFromStream();
    }
    #endregion

}
