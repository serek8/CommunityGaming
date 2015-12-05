using UnityEngine;
using System.Collections;
using CommunityGaming;

public class Player : MonoBehaviour {

    private UnityCommunicationUnit ucu;

    private Camera camera;
    private float speed = 20.0f;
    public GameObject testCube;
    private GameObject player;
    private Transform playerTransform;
    private PlayerInfo playerInfo;
    private int respawnTime = 3;
    Position currentPosition = Position.Center;
    

    private GameObject bullet; //bullet object
    private float nextFire; // set to 0 in Unity
    private float fireRate; // how often they should shot 

    public PlayerInfo getPlayerInfo()
    {
        return playerInfo;
    }

    
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bullet = Resources.Load("BulletNew") as GameObject;
       
    }
    public void InitializePlayer()
    {
        testCube = Resources.Load("Cube") as GameObject;
        player = Instantiate(testCube, new Vector3(testCube.transform.position.x + UnityEngine.Random.Range(1.0f, 3.0f), 0, 0), Quaternion.identity) as GameObject;
        player.GetComponent<SpriteRenderer>().color = new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
        playerTransform = player.GetComponent<Transform>();
        //playerTransform.SetParent(GameObject.Find("Players").GetComponent<Transform>());
        playerTransform.SetParent(ucu.gameObject.GetComponent<Transform>());
        playerInfo = player.AddComponent<PlayerInfo>();
    }

  

    public void setUnityCommunicationUnit(UnityCommunicationUnit ucu)
    {
        this.ucu = ucu;
    }
    private float FindCenter()
    {
        
        Vector2 top = new Vector2(0f, camera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y + 0.5f);
        Vector2 bottom = new Vector2(0f, camera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y - 0.5f);
        float center = top.y - ((Mathf.Abs(top.y) + Mathf.Abs(bottom.y)) / 2);
        return center;

    }

    public void movePlayer(WarriorCommunicationUnit warrior)
    {
        var move = new Vector3(Mathf.Cos((2.0f * Mathf.PI * warrior.movement) / 360f), Mathf.Sin((2.0f * Mathf.PI * warrior.movement) / 360f), 0f);
        playerTransform.position += move * 3.0f * Time.deltaTime;
    }

    public void RotateObject(int x)
    {
        playerTransform.rotation = Quaternion.Euler(0, 0, x-90);

    }
    public void ShotBullet(WarriorCommunicationUnit warrior)
    {
        //check if warrior.action is equal to shot and if they are eligble to shot later we can add types of weapon and depending on them do frequency of shooting
        if (warrior.action == 1 && warrior.isActionSet && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector3 bulletSpawnPosition = playerTransform.position + (playerTransform.rotation * Vector3.up) * 1.0f;
            GameObject b= Instantiate(bullet, bulletSpawnPosition, playerTransform.rotation)as GameObject;
            b.GetComponent<BulletMover>().setWhoShot(this);
            warrior.isActionSet = false;
        }
    }
    public void PlayerGotHit(Player p)
    {
        playerInfo.healthPoints--;
        if (playerInfo.healthPoints <= 0)
        {
            p.getPlayerInfo().incrementKillCounter();
            player.SetActive(false);
            playerInfo.ResetStats();
            playerInfo.incrementDeathCounter();
            StartCoroutine(WaitForRespawn());
        }
    }
    IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        player.SetActive(true); // need to change to spawn in direct position not on last death location.
    }
    private enum Position
    {
        Left,
        Right,
        Center
    }

    private void moveLeft()
    {
        if ((ucu.warrior.movement > 0 && ucu.warrior.movement < 60) || (ucu.warrior.movement > 300 && ucu.warrior.movement < 360))
        {
            playerTransform.Translate(new Vector3(-5, 0, 0));
            if(currentPosition==Position.Center)
            {
                currentPosition = Position.Left;
            }
            else if(currentPosition==Position.Right)
            {
                currentPosition = Position.Center;
            }
        }
    }

    private void moveRight()
    {
        if (ucu.warrior.movement > 135 && ucu.warrior.movement < 225)
        {
            playerTransform.Translate(new Vector3(5, 0, 0));
            if (currentPosition == Position.Center)
            {
                currentPosition = Position.Right;
            }
            else if (currentPosition == Position.Left)
            {
                currentPosition = Position.Right;
            }
        }
    }

    public void ChooseTeams()
    {
       if(currentPosition==Position.Left)
        {
            moveLeft();
        }
       else if(currentPosition==Position.Center)
        {
            moveLeft();
            moveRight();
        }
       else
        {
            moveRight();
        }

    }

   


}
