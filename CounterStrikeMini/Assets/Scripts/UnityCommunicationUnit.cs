using UnityEngine;
using System.Collections;
using CommunityGaming;
using System;
using System.Net.Sockets;

public class UnityCommunicationUnit : MonoBehaviour, ISocketAccessibility, IBaseSerializer
{

     WarriorCommunicationUnit warrior;
    float speed = 20.0f;
    GameObject testCube;

	// Use this for initialization
	void Start () {
        GameObject player = Instantiate(testCube, new Vector3(testCube.transform.position.x + UnityEngine.Random.Range(5.0f, 7.0f), testCube.transform.position.y + UnityEngine.Random.Range(3.0f, 5.0f), 0), Quaternion.identity) as GameObject;
        
    }

    public void setWarrior(WarriorCommunicationUnit w)
    {
        warrior = w;
    }

    // Update is called once per frame
    void Update () {

        switch(warrior.movement)
        {
            case 0:
                moveUp();
                break;
            case 1:
                moveDown();
                break;
            case 2:
                moveLeft();
                break;
            case 3:
                moveRight();
                break;
            default:
                break;
        }
	
	}

   

    public void didDisconnect()
    {
        throw new NotImplementedException();
    }

   public UnityCommunicationUnit(NetworkStream stream)
    {
      
        

    }

    void recogniseMovement()
    {

    }
    void moveUp()
    {
        var move = new Vector3(0f, 0.5f, 0f);
        transform.position += move * speed * Time.deltaTime;
        
    }
    void moveDown()
    {
        var move = new Vector3(0f, -0.5f, 0f);
        transform.position += move * speed * Time.deltaTime;
    }
    void moveLeft()
    {

        var move = new Vector3(-0.5f, 0f, 0f);
        transform.position += move * speed * Time.deltaTime;
    }
    void moveRight()
    {
        var move = new Vector3(0.5f, 0f, 0f);
        transform.position += move * speed * Time.deltaTime;
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
