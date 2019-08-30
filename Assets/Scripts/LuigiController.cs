using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuigiController : MonoBehaviour {
    
    public List<Transform> luigiPositions = new List<Transform>();
    private List<int> crateMovePositions = new List<int>() {11, 29, 47};
    
    private int luigiCurrentPosition = 2;
    
    public GameObject bottleCrate;
    
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
    }

    private void MoveLuigiUp() {

        if (luigiCurrentPosition > 0) {
            luigiCurrentPosition--;
            UpdatePosition();
        }
        
        Debug.Log("Luigi Up");
    }

    private void MoveLuigiDown() {
        if (luigiCurrentPosition < luigiPositions.Count - 1) {
            luigiCurrentPosition++;
            UpdatePosition();
        }
        
        Debug.Log("Luigi Down");
    }

    private void UpdatePosition() {
        transform.position = luigiPositions[luigiCurrentPosition].position;
    }
    
//    private void OnTriggerEnter2D(Collider2D other) {
//        int bottleCratePosition = bottleCrate.GetComponent<BottleCrateController>().bottleCrateCurrentPosition;
//        
//        Debug.Log("OnTriggerEnter2D Luigi" );
//        
//        foreach (var position in crateMovePositions) {
//            
//            if (bottleCratePosition == position && MoveCrate != null) {
//                MoveCrate();
//                break;
//            }
//        }
//        
//    }

}
