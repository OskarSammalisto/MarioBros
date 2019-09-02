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
    private bool newTruckUnpause = true;


    
    
    private void OnEnable() {
        BottleCrateController.CrateOnTruck += StackCrate;
    }

    private void OnDisable() {
        BottleCrateController.CrateOnTruck -= StackCrate;
    }

    private void Start() {
        start = Time.time;
        MoveTruckIntoPosition();
    }

    private void Update() {

        if (newTruckUnpause) {
             
             if (Time.time  >= start + newTruckPauseTime ) {
                 Debug.Log("started");
                        TruckIsReady?.Invoke();
                        newTruckUnpause = false;
             }
        }
       
    }

    private void StackCrate() {
        if (numberOfCrates < positionsOnTruck.Count ) {
             cratesOnTruck[numberOfCrates].SetActive(true);
                    LeanTween.move(cratesOnTruck[numberOfCrates], positionsOnTruck[numberOfCrates].transform.position, time: crateLoadSpeed); 
                  //  cratesOnTruck[numberOfCrates].transform.position = positionsOnTruck[numberOfCrates].transform.position;
                    numberOfCrates++;
                    Debug.Log(numberOfCrates);
        }
       

        if (numberOfCrates >= positionsOnTruck.Count) {
            TruckIsFull?.Invoke();
            StartCoroutine(MoveTruck());
            StartCoroutine(DestroyTruck());
           // Debug.Log("Truck is Full");
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
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }
}
