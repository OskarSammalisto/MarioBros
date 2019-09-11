using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuigiController : MonoBehaviour {
    
    public List<Transform> luigiPositions = new List<Transform>();
    private List<int> crateMovePositions = new List<int>() {11, 29, 47};
    public List<Sprite> luigiSprites = new List<Sprite>();
    
    
    private int luigiCurrentPosition = 2;
    public Transform luigiBreakPosition;
    
    
    public GameObject bottleCrate;
    public GameObject luigiDoor;
    public GameObject luigiBoss;
    public SoundManager soundManager;
    private SpriteRenderer spriteRenderer;
    private bool disableInput = false;
    
//    //move box by trigger
//    public delegate void LuigiMoveCrate();
//
//    public static event LuigiMoveCrate MoveCrate;
    
    private void OnEnable() {
        MoveInput.LuigiUp += MoveLuigiUp;
        MoveInput.LuigiDown += MoveLuigiDown;

    }

    private void OnDisable() {
        MoveInput.LuigiUp -= MoveLuigiUp;
        MoveInput.LuigiDown -= MoveLuigiDown;
    }

    private void Start() {
       
        UpdatePosition();
        spriteRenderer = GetComponent<SpriteRenderer>();
        luigiBoss.SetActive(false);
    }

    private void MoveLuigiUp() {

        if (luigiCurrentPosition > 0 && !disableInput) {
            luigiCurrentPosition--;
            soundManager.PlayJump();
            UpdatePosition();
        }
        //  Debug.Log("Luigi Up");,
    }

    private void MoveLuigiDown() {
        if (luigiCurrentPosition < luigiPositions.Count - 2 && !disableInput) {
            soundManager.PlayJump();
            luigiCurrentPosition++;
            UpdatePosition();
        }
        // Debug.Log("Luigi Down");
    }

    private void UpdatePosition() {
        transform.position = luigiPositions[luigiCurrentPosition].position;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        soundManager.PlayMoveShell();
        if (luigiCurrentPosition == 0) {
            StartCoroutine(LuigiMoveShellTop());
        }
        else {
            StartCoroutine(LuigiMoveShell());
        }
        
    }

    IEnumerator LuigiMoveShellTop() {
        
        spriteRenderer.sprite = luigiSprites[1];
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = luigiSprites[0];
        yield return new WaitForSeconds(0.1f);
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        
    }

    IEnumerator LuigiMoveShell() {
        spriteRenderer.sprite = luigiSprites[1];
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.sprite = luigiSprites[0];
    }

    public IEnumerator LuigiBreakAnimation() {
        soundManager.PlayLoseLife();
        disableInput = true;
        transform.position = luigiBreakPosition.position;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        luigiBoss.SetActive(true);
        luigiDoor.GetComponent<DoorController>().OpenDoor();
        float time = Time.time;
        while (time >= Time.time - 5f) {
            spriteRenderer.sprite = luigiSprites[4];
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.sprite = luigiSprites[5];
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.sprite = luigiSprites[6];
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.sprite = luigiSprites[5];

        }
       
        luigiDoor.GetComponent<DoorController>().CloseDoor();
        luigiBoss.SetActive(false);
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        spriteRenderer.sprite = luigiSprites[0];
        UpdatePosition();
        disableInput = false;
    }
    

}
