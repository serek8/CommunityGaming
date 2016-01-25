using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

    public GameObject mapC;
    public GameObject timer;

    //private void SetTeams()
    //{
    //    for()
    //}
    public void StartTheGame()
    {
        GameObject left = GameObject.Find("a");
        left.SetActive(false);
        GameObject right = GameObject.Find("b");
        right.SetActive(false);
        GameObject mainSt = GameObject.Find("mainStage");
        mainSt.SetActive(false);
        GameObject.Find("LEFT").SetActive(false);
        GameObject.Find("RIGHT").SetActive(false);
        GameObject.Find("StartGameButton").SetActive(false);
        timer.SetActive(true);
        GameObject.Find("Timer").GetComponent<Timer>().StartTimer();
        
        mapC.SetActive(true);
    }
}
