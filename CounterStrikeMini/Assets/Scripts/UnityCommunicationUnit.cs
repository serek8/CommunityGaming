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



    // Use this for initialization
    void Start () {
        testCube = Resources.Load("Cube") as GameObject;
       player=  Instantiate(testCube, new Vector3(testCube.transform.position.x + UnityEngine.Random.Range(5.0f, 7.0f), testCube.transform.position.y + UnityEngine.Random.Range(3.0f, 5.0f), 0), Quaternion.identity)as GameObject ;
        playerTransform = player.GetComponent<Transform>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void setWarrior(WarriorCommunicationUnit w)
    {
        warrior = w;
        
    }
 

    // Update is called once per frame
    void Update () {

        switch (warrior.movement)
        {
            case 0:
                moveUp(playerTransform);
                break;
            case 1:
                moveDown(playerTransform);
                break;
            case 2:
                moveLeft(playerTransform);
                break;
            case 3:
                moveRight(playerTransform);
                break;
            default:
                break;
        }
        RotateObject(warrior.rotation);

    }

   

    public void didDisconnect()
    {
        throw new NotImplementedException();
    }

   public UnityCommunicationUnit(NetworkStream stream)
    {
      
        

    }


    void moveUp(Transform player)
    {
        var move = new Vector3(0f, 0.5f, 0f);
        player.position += move *3.0f * Time.deltaTime;

    }
    void moveDown(Transform player)
    {
        var move = new Vector3(0f, -0.5f, 0f);
        player.position += move * 3.0f * Time.deltaTime;

    }
    void moveLeft(Transform player)
    {
        var move = new Vector3(-0.5f, 0f, 0f);
        player.position += move * 3.0f * Time.deltaTime;

    }
    void moveRight(Transform player)
    {
        var move = new Vector3(0.5f, 0f, 0f);
        player.position += move * 3.0f * Time.deltaTime;
        

    }

    void RotateObject(int x)
    {

       
      playerTransform.rotation = Quaternion.Euler(0, 0, x);
     
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
