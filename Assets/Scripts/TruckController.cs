using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour {

    public delegate void TruckStatus();

    public static event TruckStatus TruckIsFull;
    public static event TruckStatus TruckIsReady;

    public List<GameObject> cratesOnTruck = new List<GameObject>();
    public List<Transform> positionsOnTruck = new List<Transform>();
    public List<Transform> truckPositions = new List<Transform>();

    private int numberOfCrates = 0;
    private float start;
    private float crateLoadSpeed = 1.0f;
    private float truckMoveSpeed = 1.0f;
    private float newTruckSpeed = 7f;
    private float truckDelayMoveTruck = 1.2f;
    private float newTruckPauseTime = 7.0f;
    private float destroyTruckDelay = 6.0f;
    private bool newTruckUnpause = true;

    private SoundManager soundManager;
    


    
    
    private void OnEnable() {
        BottleCrateController.CrateOnTruck += StackCrate;
    }

    private void OnDisable() {
        BottleCrateController.CrateOnTruck -= StackCrate;
    }

    private void Start() {
         GameObject soundMngrObj = GameObject.FindWithTag("SoundManager");
        soundManager = soundMngrObj.GetComponent<SoundManager>();
        start = Time.time;
        MoveTruckIntoPosition();
        
        //Debug.Log(" ");
    }

    private void Update() {

        if (newTruckUnpause) {
             
             if (Time.time  >= start + newTruckPauseTime ) {
                 
                        TruckIsReady?.Invoke();
                        newTruckUnpause = false;
             }
        }
       
    }

    private void StackCrate() {
        if (numberOfCrates < positionsOnTruck.Count ) {
             cratesOnTruck[numberOfCrates].SetActive(true);
                    LeanTween.move(cratesOnTruck[numberOfCrates], positionsOnTruck[numberOfCrates].transform.position, time: crateLoadSpeed); 
                 
                    numberOfCrates++;
                    Debug.Log(numberOfCrates);
        }
       

        if (numberOfCrates >= positionsOnTruck.Count) {
            TruckIsFull?.Invoke();
            StartCoroutine(MoveTruck());
            soundManager.PlayAirshipClear();
            StartCoroutine(DestroyTruck());
          
        }
       
    }

    IEnumerator MoveTruck() {
        yield return new WaitForSeconds(truckDelayMoveTruck);
        LeanTween.move(gameObject, truckPositions[1].transform.position, time: truckMoveSpeed);
        
        
    }

    private void MoveTruckIntoPosition() {
        GameObject o;
        (o = gameObject).transform.position = truckPositions[1].transform.position;
        LeanTween.move(o, truckPositions[0].transform.position, newTruckSpeed);
    }

    IEnumerator DestroyTruck() {
        yield return new WaitForSeconds(destroyTruckDelay);
        Destroy(gameObject);
    }
}
