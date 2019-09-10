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
  public GameObject gameOverText;
  private DoorController doorController;
  private BossController bossController;
  public Text scoreText;
  public Text missText;
  public List<String> missTextList = new List<string>(){"","Miss X", "Miss X X", "Miss X X X"};
  
  [SerializeField]
  private int score;
  
  private int misses;

  private int missResetScore = 300;
  public float crateSpawnRate = 12f;
  private float crateSpawnRateModifier = 0.0015f;
  private float maxSpawnRate = 2.5f;
  private float pauseDurationOnCrateBreak = 6.0f;

  public MarioController marioController;
  public LuigiController luigiController;
  
  
  private bool gameOver;
  private bool pause = false;
  
   
   private void OnEnable() {
       BottleCrateController.LoseLifeMario += MarioMiss;
       BottleCrateController.LoseLifeLuigi += LuigiMiss;
       BottleCrateController.IncreaseScore += GetPoints;
       TruckController.TruckIsFull += SpawnTruck;
       TruckController.TruckIsReady += TruckReady;
   }

  

   private void OnDisable() {
       BottleCrateController.LoseLifeMario -= MarioMiss;
       BottleCrateController.LoseLifeLuigi -= LuigiMiss;
       BottleCrateController.IncreaseScore -= GetPoints;
       TruckController.TruckIsFull -= SpawnTruck;
       TruckController.TruckIsReady -= TruckReady;
   }

   private void Start() {
       gameOver = false;
       score = 0;
       misses = 0;
       gameOverText.SetActive(false);
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

   private void MarioMiss() {
       StartCoroutine(LifeLost());
       StartCoroutine(marioController.MarioBreakAnimation());
   }

   private void LuigiMiss() {
       StartCoroutine(LifeLost());
       StartCoroutine(luigiController.LuigiBreakAnimation());
   }

   private IEnumerator LifeLost() {
       misses++;
       SetMissText();
       Pause?.Invoke();
       pause = true;
       

       if (misses == 3) {
           gameOver = true;
           pause = true;
           Pause?.Invoke();
           gameOverText.SetActive(true);
           //  Debug.Log("Game Over");
       }
       yield return new WaitForSeconds(pauseDurationOnCrateBreak);
       UnPause?.Invoke();
       Debug.Log("unpaused");
       pause = false;
       //Debug.Log("Lost Life");
   }

   private void GetPoints() {
       score++;
       SetScoreText();
       DifficultyAdjuster();
       if (score == missResetScore) {
           misses = 0;
           SetMissText();
       }

       
   }

   private void DifficultyAdjuster() {
       if (crateSpawnRate >= maxSpawnRate) {
            crateSpawnRate = crateSpawnRate - (crateSpawnRateModifier * score);
       }
      
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
       Instantiate(bottleCrate);
       UnPause?.Invoke();
   }
   
   
   
}
