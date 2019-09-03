using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCrateController : MonoBehaviour {

    public delegate void ScoreBoard();

    public static event ScoreBoard IncreaseScore;
    public static event ScoreBoard LoseLife;
    public static event ScoreBoard CrateOnTruck;
    
    //move trigger to crate
    private List<int> crateMovePositions = new List<int>() {2, 11, 20, 29, 38, 47};
    private int crateToTruckPosition = 48;
    
    
    public List<Transform> bottleCratePositions = new List<Transform>();
    public List<Transform> brokenCratePositions = new List<Transform>();
//    public GameObject mario;
//    public GameObject luigi;
    public int bottleCrateCurrentPosition = 0;
    public float crateSpeed = 1.5f;
    
    private bool pause = false;
    private bool broken;

    private void OnEnable() {
        GameController.Pause += Pause;
        GameController.UnPause += UnPause;
    }

    private void OnDisable() {
        GameController.Pause -= Pause;
        GameController.UnPause -= UnPause;
    }

    private void Start() {
        UpdatePosition();
        broken = false;

        StartCoroutine(MoveCrate());
    }

    private IEnumerator MoveCrate() {
        
            for (int i = 0; i < bottleCratePositions.Count;) {
                        
                        if (broken) {
                            break;
                            
                        }

                        if (pause) {
                            yield return new WaitForSeconds(crateSpeed);
                        }
                        
                        //TODO: God Mode On .....
                        
//                        else if ((i == 3 && bottleCrateCurrentPosition == 2) || (i == 21 && bottleCrateCurrentPosition == 20) || 
//                            (i == 39 && bottleCrateCurrentPosition == 38)) {
//                            BrokenCrate(true);
//                            LoseLife?.Invoke();
//                            break;
//                        }
//                        else if ((i == 12 && bottleCrateCurrentPosition == 11) || (i == 30 && bottleCrateCurrentPosition == 29) ||
//                                 (i == 48 && bottleCrateCurrentPosition == 47)) {
//                            BrokenCrate(false);
//                            LoseLife?.Invoke();
//                            break;
//                        }
                        else {
                            bottleCrateCurrentPosition = i;
                            UpdatePosition();
                            i++;
                            yield return new WaitForSeconds(crateSpeed);
                        }
                        
            }
        
        
        
    }

    private void UpdatePosition() {
        
        if (!broken) {
             transform.position = bottleCratePositions[bottleCrateCurrentPosition].position;
             if (bottleCrateCurrentPosition == crateToTruckPosition) {

                 GameObject parent = transform.parent.gameObject;
                 Destroy(parent);
                 CrateOnTruck?.Invoke();
                // Debug.Log("Crate is on truck!");
             }
        }
       

    }

    private void BrokenCrate(bool marioBroke) {
        broken = true;
        if (marioBroke) {
          //  Debug.Log("mario broke it");
            transform.position = brokenCratePositions[0].position;
        }
        else {
          //  Debug.Log("luigi broke it");
            transform.position = brokenCratePositions[1].position;
        }
    }

    private void PlayerMoveCrate() {
        bottleCrateCurrentPosition++;
        UpdatePosition();
        IncreaseScore?.Invoke();
       // Debug.Log("Player Move Crate");
    }
    
    private void OnTriggerEnter2D(Collider2D other) {

//       Debug.Log(other.name );
        
        foreach (var position in crateMovePositions) {
            
            if (bottleCrateCurrentPosition == position) {
               PlayerMoveCrate();
               break;
            }
        }
        
    }

    private void Pause() {
        pause = true;
    }

    private void UnPause() {
        pause = false;
    }
}
