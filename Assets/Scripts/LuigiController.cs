using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuigiController : MonoBehaviour {
    
    public List<Transform> luigiPositions = new List<Transform>();
    private List<int> crateMovePositions = new List<int>() {11, 29, 47};
    public List<Sprite> luigiSprites = new List<Sprite>();
    
    
    private int luigiCurrentPosition = 2;
    
    
    public GameObject bottleCrate;
    private SpriteRenderer spriteRenderer;
    
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
    }

    private void MoveLuigiUp() {

        if (luigiCurrentPosition > 0) {
            luigiCurrentPosition--;
            UpdatePosition();
        }
        //  Debug.Log("Luigi Up");
    }

    private void MoveLuigiDown() {
        if (luigiCurrentPosition < luigiPositions.Count - 1) {
            luigiCurrentPosition++;
            UpdatePosition();
        }
        // Debug.Log("Luigi Down");
    }

    private void UpdatePosition() {
        transform.position = luigiPositions[luigiCurrentPosition].position;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
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

}
