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
            player.ShotWithRotation(warrior.rotation);
            }
            //player.ShotBullet(warrior);
        
        //else
        //{
        //    player.ChooseTeams();
        //}
    }

    public void PlayerGotHit(Player p)
    {
        player.PlayerGotHit(p);
    }
      
    #region Serverside methods
    

    public UnityCommunicationUnit(NetworkStream stream) {

    }

    public void readFromStream() {
        ((IBaseSerializer)warrior).readFromStream();
    }

    public void writeFromStream() {
        ((IBaseSerializer)warrior).writeFromStream();
    }

    public void AdoptNewData(byte[] bytes)
    {
        ((IBaseSerializer)warrior).AdoptNewData(bytes);
    }

    public void didDisconnect()
    {
        ((ISocketAccessibility)warrior).didDisconnect();
    }
    #endregion

}
