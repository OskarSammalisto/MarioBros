using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour {

    public List<GameObject> cratesOnTruck = new List<GameObject>();
    public List<Transform> positionsOnTruck = new List<Transform>();
    public List<Transform> truckPositions = new List<Transform>();

    private int numberOfCrates = 0;


    
    
    private void OnEnable() {
        BottleCrateController.CrateOnTruck += StackCrate;
    }

    private void OnDisable() {
        BottleCrateController.CrateOnTruck -= StackCrate;
    }

    private void Start() {
        GameObject o;
        (o = gameObject).transform.position = truckPositions[1].transform.position;
        LeanTween.move(o, truckPositions[0].transform.position, 1f);
    }

//    private void Update() {
//        float start = Time.time;
//        if (Time.time >= start + 1.0f) {
//            LeanTween.move(gameObject, truckPositions[0].transform.position, 1.5f);
//        }
//    }

    private void StackCrate() {
        cratesOnTruck[numberOfCrates].SetActive(true);
        LeanTween.move(cratesOnTruck[numberOfCrates], positionsOnTruck[numberOfCrates].transform.position, time: 1f); 
      //  cratesOnTruck[numberOfCrates].transform.position = positionsOnTruck[numberOfCrates].transform.position;
        numberOfCrates++;
        Debug.Log(numberOfCrates);

        if (numberOfCrates >= positionsOnTruck.Count) {
            StartCoroutine(MoveTruck());
         //   LeanTween.move(gameObject, truckPositions[1].transform.position, time: 2f);
            Debug.Log("Truck is Full");
        }
        // Debug.Log("Truck crate invoke!");
    }

    IEnumerator MoveTruck() {
        yield return new WaitForSeconds(2);
        LeanTween.move(gameObject, truckPositions[1].transform.position, time: 1f);
    }
}
