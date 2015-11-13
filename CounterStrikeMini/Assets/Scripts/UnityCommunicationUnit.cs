using UnityEngine;
using System.Collections;
using CommunityGaming;
using System;
using System.Net.Sockets;

public class UnityCommunicationUnit : MonoBehaviour, ISocketAccessibility, IBaseSerializer
{

   public  WarriorCommunicationUnit warrior;
    float speed = 20.0f;
    public GameObject testCube;
    private GameObject player;
    private Transform playerTransform;

    private Camera camera;
    private Vector3 mousePosition;
    private Vector3 direction;
    private float distanceFromObject;

    public GameObject bullet; //bullet object
    public float nextFire; // set to 0 in Unity
    public float fireRate; // how often they should shot 


    // Use this for initialization
    void Start () {
        testCube = Resources.Load("Cube") as GameObject;
       player=  Instantiate(testCube, new Vector3(testCube.transform.position.x + UnityEngine.Random.Range(5.0f, 7.0f), testCube.transform.position.y + UnityEngine.Random.Range(3.0f, 5.0f), 0), Quaternion.identity)as GameObject ;
//		player.GetComponent<SpriteRenderer>().material.color = Color(UnityEngine.Random.Range (0.0, 1.0), UnityEngine.Random.Range (0.0, 1.0), UnityEngine.Random.Range (0.0, 1.0));
		player.GetComponent<SpriteRenderer>().color = new Color(UnityEngine.Random.Range(0.0f,1.0f),UnityEngine.Random.Range(0.0f,1.0f),UnityEngine.Random.Range(0.0f,1.0f));
			playerTransform = player.GetComponent<Transform>();
        playerTransform.SetParent(GameObject.Find("Players").GetComponent<Transform>());
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		bullet = Resources.Load ("Bullet") as GameObject;
    }

    public void setWarrior(WarriorCommunicationUnit w)
    {
        warrior = w;
        
    }
 

    // Update is called once per frame
    void Update () {

//        switch (warrior.movement)
//        {
//            case 0:
//                moveUp(playerTransform);
//                break;
//            case 1:
//                moveDown(playerTransform);
//                break;
//            case 2:
//                moveLeft(playerTransform);
//                break;
//            case 3:
//                moveRight(playerTransform);
//                break;
//            default:
//                break;
//        }
		if (warrior.movement != -1) {
			movePlayer ();
		}
		if (warrior.rotation != -1) {
			RotateObject (warrior.rotation);
		}
        ShotBullet();

    }

   

    public void didDisconnect()
    {
        throw new NotImplementedException();
    }

   public UnityCommunicationUnit(NetworkStream stream)
    {
      
        

    }

	void movePlayer()
	{
		var move = new Vector3 (Mathf.Cos ((2.0f * Mathf.PI * warrior.movement) / 360f), Mathf.Sin ((2.0f * Mathf.PI * warrior.movement) / 360f), 0f);
		playerTransform.position += move * 3.0f * Time.deltaTime;
	}


//    void moveUp(Transform player)
//    {
//        var move = new Vector3(0f, 0.5f, 0f);
//        player.position += move *3.0f * Time.deltaTime;
//
//    }
//    void moveDown(Transform player)
//    {
//        var move = new Vector3(0f, -0.5f, 0f);
//        player.position += move * 3.0f * Time.deltaTime;
//
//    }
//    void moveLeft(Transform player)
//    {
//        var move = new Vector3(-0.5f, 0f, 0f);
//        player.position += move * 3.0f * Time.deltaTime;
//
//    }
//    void moveRight(Transform player)
//    {
//        var move = new Vector3(0.5f, 0f, 0f);
//        player.position += move * 3.0f * Time.deltaTime;
//        
//
//    }

    void RotateObject(int x)
    {
      playerTransform.rotation = Quaternion.Euler(0, 0, x);
     
    }

    void ShotBullet() {
        //check if warrior.action is equal to shot and if they are eligble to shot later we can add types of weapon and depending on them do frequency of shooting
        if(warrior.action == 1 && warrior.isActionSet&& Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Vector3 bulletSpawnPosition = playerTransform.position + new Vector3(0.2f, 0.0f, 0.0f);
            Instantiate(bullet, bulletSpawnPosition, bullet.transform.rotation);
            warrior.isActionSet = false;
        }
    }



    public void readFromStream()
    {
        ((IBaseSerializer)warrior).readFromStream();
    }

    public void writeFromStream()
    {
        ((IBaseSerializer)warrior).writeFromStream();
    }
}
