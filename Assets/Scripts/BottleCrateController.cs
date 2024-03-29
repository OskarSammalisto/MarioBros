﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCrateController : MonoBehaviour {

    public delegate void ScoreBoard();

    public static event ScoreBoard IncreaseScore;
    public static event ScoreBoard LoseLifeMario;
    public static event ScoreBoard LoseLifeLuigi;
    public static event ScoreBoard CrateOnTruck;

    //move trigger to crate
    private List<int> crateMovePositions = new List<int>() {2, 11, 20, 29, 38, 47};
    public List<Sprite> crateSprites = new List<Sprite>();
    private int crateToTruckPosition = 48;
    private int spawnConveyorBeltLength = 3;
    private SpriteRenderer spRenderer;
    
    
    public List<Transform> bottleCratePositions = new List<Transform>();
    public List<Transform> brokenCratePositions = new List<Transform>();

    public int bottleCrateCurrentPosition = 0;
    public float crateSpeed = 1.2f;
    
    private bool pause = false;
    private bool broken;
    private bool walkToggle = false;

    private void OnEnable() {
        GameController.Pause += Pause;
        GameController.UnPause += UnPause;
    }

    private void OnDisable() {
        GameController.Pause -= Pause;
        GameController.UnPause -= UnPause;
    }

    private void Start() {
        spRenderer = GetComponent<SpriteRenderer>();
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
                        
                        else if ((i == 3 && bottleCrateCurrentPosition == 2) || (i == 21 && bottleCrateCurrentPosition == 20) || 
                            (i == 39 && bottleCrateCurrentPosition == 38)) {
                            BrokenCrate(true);
                            LoseLifeMario?.Invoke();
                            break;
                        }
                        else if ((i == 12 && bottleCrateCurrentPosition == 11) || (i == 30 && bottleCrateCurrentPosition == 29) ||
                                 (i == 48 && bottleCrateCurrentPosition == 47)) {
                            BrokenCrate(false);
                            LoseLifeLuigi?.Invoke();
                            break;
                        }
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
                 CrateOnTruck?.Invoke();
                 Destroy(parent);
             }
             else if (bottleCrateCurrentPosition >= spawnConveyorBeltLength) {
                 walkToggle = !walkToggle;
                 WalkAnimation(walkToggle);
             }
        }
       

    }

    private void WalkAnimation(bool walk) {

        if (walk) {
            spRenderer.sprite = crateSprites[1];
        }
        else {
            spRenderer.sprite = crateSprites[0];
        }
        
        
    }

    private void BrokenCrate(bool marioBroke) {
        broken = true;
        spRenderer.sprite = crateSprites[2];
        if (marioBroke) {
          
          transform.position = brokenCratePositions[0].position;
        }
        else {
          
          transform.position = brokenCratePositions[1].position;
        }
    }

    private void PlayerMoveCrate() {
        bottleCrateCurrentPosition++;
        UpdatePosition();
        spRenderer.flipX = !spRenderer.flipX;
        IncreaseScore?.Invoke();
       
    }
    
    private void OnTriggerEnter2D(Collider2D other) {


        
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
