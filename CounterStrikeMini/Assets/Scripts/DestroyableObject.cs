using UnityEngine;
using System.Collections;


namespace CommunityGaming {
    public class DestroyableObject: MonoBehaviour {

        public int hp;
        public Sprite dmgSprite;

       // private SpriteRenderer spriteRenderer;

        void Awake() {
            //spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void DamgeObject (int loss) {
          //  spriteRenderer.sprite = dmgSprite;
            hp -= loss;
            Debug.Log(hp);
            if(hp <= 0) {
                gameObject.SetActive(false);
            }
        }
        void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Bullet") {
                Destroy(other.gameObject);
                DamgeObject(1);
            }
        }
    }



  
}


