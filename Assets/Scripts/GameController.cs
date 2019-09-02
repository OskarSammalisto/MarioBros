using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    
    public delegate void Manager();

    public static event Manager Pause;
    public static event Manager UnPause;
    
    
  public GameObject bottleCrate;
  public GameObject truck;
  public Text scoreText;
  public Text missText;
  public List<String> missTextList = new List<string>(){"","Miss X", "Miss X X", "Miss X X X"};
  
  private int score;
  private int misses;
  public float crateSpawnRate = 5f;
  
  
  private bool gameOver;
  private bool pause = false;
  
   
   private void OnEnable() {
       BottleCrateController.LoseLife += LifeLost;
       BottleCrateController.IncreaseScore += GetPoints;
       TruckController.TruckIsFull += SpawnTruck;
       TruckController.TruckIsReady += TruckReady;
   }

  

   private void OnDisable() {
       BottleCrateController.LoseLife -= LifeLost;
       BottleCrateController.IncreaseScore -= GetPoints;
       TruckController.TruckIsFull -= SpawnTruck;
       TruckController.TruckIsReady -= TruckReady;
   }

   private void Start() {
       gameOver = false;
       score = 0;
       misses = 0;
       SetScoreText();
       SetMissText();
       SpawnTruck();
      StartCoroutine(CreateCrate());
   }
   
   private void SpawnTruck() {
       
       if (!gameOver) {
           pause = true;
           Pause?.Invoke();
           Debug.Log("new truck");
           Instantiate(truck);
       }
   }

   private IEnumerator CreateCrate() {
       while (!gameOver) {
//           GameObject crate = Instantiate<GameObject>(bottleCrate);
//           crate.SetActive(true);
           if (!pause) {
               Instantiate(bottleCrate);
           }
           
           yield return new WaitForSeconds(crateSpawnRate);
       }
       
   }


   private void LifeLost() {
       misses++;
       SetMissText();

       if (misses == 3) {
           gameOver = true;
           pause = true;
           Pause?.Invoke();
           //  Debug.Log("Game Over");
       }
       //Debug.Log("Lost Life");
   }

   private void GetPoints() {
       score++;
       SetScoreText();
      // Debug.Log("Got A Point");
       
   }

   private void SetScoreText() {
       scoreText.text = score.ToString();
   }

   private void SetMissText() {
       if (misses <= 3) {
           missText.text = missTextList[misses];
       }
       
   }

   void TruckReady() {
       pause = false;
       UnPause?.Invoke();
   }
   
   
   
}
