﻿using UnityEngine;
using System.Collections;
using CommunityGaming;
using System;
using System.Net.Sockets;

public class UnityCommunicationUnit : MonoBehaviour, ISocketAccessibility, IBaseSerializer {

    #region UnityCommunicationUnit variables
    public WarriorCommunicationUnit warrior;
    float speed = 20.0f;
    public GameObject testCube;
    private GameObject player;
    private Transform playerTransform;
    private float respawnTime;

    private Camera camera;
    private Vector3 mousePosition;
    private Vector3 direction;
    private float distanceFromObject;

    public GameObject bullet; //bullet object
    public float nextFire; // set to 0 in Unity
    public float fireRate; // how often they should shot 

    private PlayerInfo playerInfo;
    #endregion


    // Use this for initialization
    void Start () {
        InitializePlayer();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		bullet = Resources.Load ("BulletNew") as GameObject;
    }

    private void InitializePlayer() {
        testCube = Resources.Load("Cube") as GameObject;
        player = Instantiate(testCube, new Vector3(testCube.transform.position.x + UnityEngine.Random.Range(5.0f, 7.0f), testCube.transform.position.y + UnityEngine.Random.Range(3.0f, 5.0f), 0), Quaternion.identity) as GameObject;
        player.GetComponent<SpriteRenderer>().color = new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
        playerTransform = player.GetComponent<Transform>();
        //playerTransform.SetParent(GameObject.Find("Players").GetComponent<Transform>());
        playerTransform.SetParent(this.gameObject.GetComponent<Transform>());
       playerInfo= player.AddComponent<PlayerInfo>();
    }

    public void setWarrior(WarriorCommunicationUnit w)
    {
        warrior = w;     
    }

    // Update is called once per frame
    void Update () {
		if (warrior.movement != -1) {
			movePlayer ();
		}
		if (warrior.rotation != -1) {
			RotateObject (warrior.rotation);
		}
        ShotBullet();
    }

    #region Player control methods
    void movePlayer()
	{
		var move = new Vector3 (Mathf.Cos ((2.0f * Mathf.PI * warrior.movement) / 360f), Mathf.Sin ((2.0f * Mathf.PI * warrior.movement) / 360f), 0f);
		playerTransform.position += move * 3.0f * Time.deltaTime;
	}

    void RotateObject(int x)
    {
      playerTransform.rotation = Quaternion.Euler(0, 0, x);
     
    }

    void ShotBullet() {
        //check if warrior.action is equal to shot and if they are eligble to shot later we can add types of weapon and depending on them do frequency of shooting
        if(warrior.action == 1 && warrior.isActionSet&& Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Vector3 bulletSpawnPosition = playerTransform.position+ (playerTransform.rotation * Vector3.up)*1.0f;

            Instantiate(bullet, bulletSpawnPosition, playerTransform.rotation);
            warrior.isActionSet = false;
        }
    }

    public void PlayerGotHit() {
        playerInfo.healthPoints--;
        if(playerInfo.healthPoints <= 0) {
            DestroyObject(player);
            playerInfo.ResetStats();
            StartCoroutine(WaitForRespawn());
        }
    }

    IEnumerator WaitForRespawn() {
        yield return new WaitForSeconds(respawnTime);
        InitializePlayer();
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Bullet")
    //    {
    //        PlayerGotHit();
    //        player.on
    //    }
    //    Destroy(other.gameObject);
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entering");
        //if()
    }
    //void OnTriggerExit2D(Collider2D other)
    //{
    //    Debug.Log("Leaving");
    //}
    //void OnTriggerStay2D(Collider2D other)
    //{
    //    Debug.Log("Staying");
    //}

    #endregion

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
