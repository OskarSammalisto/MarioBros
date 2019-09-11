using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
    
    [SerializeField]
    private List<Sprite> bossSprites = new List<Sprite>();
    
    private SpriteRenderer spriteRenderer;
    private float animationSpeed = 0.6f;
    private float lastTime;
    public bool goingUp = true;
    public int spriteIndex;
    
   
    void Start() {
        lastTime = Time.time;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update() {
        if (lastTime + animationSpeed <= Time.time) {
            lastTime = Time.time;
            spriteRenderer.sprite = bossSprites[spriteIndex];
            if (spriteIndex == 2) {
                goingUp = false;
            }
            else if(spriteIndex == 0) {
                goingUp = true;
            }
            if (goingUp) {
                spriteIndex++;
            }
            else {
                spriteIndex--;
            }
        }
    }
}
