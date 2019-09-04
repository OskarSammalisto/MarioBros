using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour {
    public List<Transform> marioPositions = new List<Transform>();
    private List<int> crateMovePositions = new List<int>() {2, 20, 38};


    public List<Sprite> marioSprites = new List<Sprite>();

    public GameObject bottleCrate;
   
//    //move box by trigger
//    public delegate void MarioMoveCrate();
//
//    public static event MarioMoveCrate MoveCrate;
    
    

    public int marioCurrentPosition = 1;

    private void OnEnable() {
        MoveInput.MarioUp += MoveMarioUp;
        MoveInput.MarioDown += MoveMarioDown;

    }

    private void OnDisable() {
        MoveInput.MarioUp -= MoveMarioUp;
        MoveInput.MarioDown -= MoveMarioDown;

    }

    private void Start() {
        UpdatePosition();
    }

    private void MoveMarioUp() {

        if (marioCurrentPosition > 0) {
            marioCurrentPosition--;
            UpdatePosition();
        }
        
      //  Debug.Log("Mario Up");
    }

    private void MoveMarioDown() {

        if (marioCurrentPosition < marioPositions.Count - 1) {
            marioCurrentPosition++;
            UpdatePosition();
        }
        
      //  Debug.Log("Mario Down");
    }

    private void UpdatePosition() {
        transform.position = marioPositions[marioCurrentPosition].position;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (marioCurrentPosition == 2) {
            StartCoroutine(MarioMoveShellBottom());
        }
        else {
            StartCoroutine(MarioMoveShell());
        }
        
    }

    IEnumerator MarioMoveShellBottom() {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        GetComponent<SpriteRenderer>().sprite = marioSprites[2];
        yield return new WaitForSeconds(0.1f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = marioSprites[0];
        
    }

    IEnumerator MarioMoveShell() {
        GetComponent<SpriteRenderer>().sprite = marioSprites[2];
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().sprite = marioSprites[0];
    }
    
    
}
