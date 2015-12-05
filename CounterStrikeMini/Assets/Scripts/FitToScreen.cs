using UnityEngine;
using System.Collections;

public class FitToScreen : MonoBehaviour {
   public SpriteRenderer sr;
    void Start()
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        GameObject mainStage = GameObject.Find("mainStage");
        SpriteRenderer sr = mainStage.GetComponent<SpriteRenderer>();

        mainStage.transform.localScale = new Vector2(
            worldScreenWidth / sr.sprite.bounds.size.x,
            worldScreenHeight / sr.sprite.bounds.size.y);
    }
}
