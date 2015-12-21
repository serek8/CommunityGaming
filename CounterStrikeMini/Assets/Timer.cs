using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float time = 0;
    private float timer = 0;
    public Text text;
    private bool started;
    

    
   public void StartTimer()
    {
        timer += Time.deltaTime + 900;
        started = true;
    }

    void Update()
    {
        if (started)
        {
            time += Time.deltaTime;
            text.text = "Time left: " + (int)(timer - time);

            if (timer - time <= 0)
            {
                Time.timeScale = 0;
            }
        }
    }

}
