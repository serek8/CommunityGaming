using UnityEngine;
using System.Collections;

public class ShowSplashScreen : MonoBehaviour {

   
       public Texture2D yourTexture;
    void onGui()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), yourTexture);
    }
    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 150, 100), "I am a button"))
    //        print("You clicked the button!");

    //}
}
