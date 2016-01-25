using UnityEngine;
using System.Collections;


namespace CommunityGaming {
    public class DestroyableObject: MonoBehaviour {

        public int hp;
        public Sprite dmgSprite;
        Color color = new Color(0.0f, 1.0f, 0.0f);

        private SpriteRenderer spriteRenderer;

        void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = color;
        }

        public void DamgeObject (int loss) {
          //  spriteRenderer.sprite = dmgSprite;
            hp -= loss;
            if(hp > 5) {
                color.r += 0.2f;
            }else {
                color.g -= 0.2f;
            }
            spriteRenderer.color = color;
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


