using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    
    
  public GameObject bottleCrate;
  public Text scoreText;
  public Text missText;
  public List<String> missTextList = new List<string>(){"","Miss X", "Miss X X", "Miss X X X"};
  
  private int score;
  private int misses;
  public float crateSpawnRate = 5f;
  
  
  private bool gameOver;
  
   
   private void OnEnable() {
       BottleCrateController.LoseLife += LifeLost;
       BottleCrateController.IncreaseScore += GetPoints;
   }

   private void OnDisable() {
       BottleCrateController.LoseLife -= LifeLost;
       BottleCrateController.IncreaseScore -= GetPoints;
   }

   private void Start() {
       gameOver = false;
       score = 0;
       misses = 0;
       SetScoreText();
       SetMissText();

      StartCoroutine(CreateCrate());
   }

   private IEnumerator CreateCrate() {
       while (!gameOver) {
//           GameObject crate = Instantiate<GameObject>(bottleCrate);
//           crate.SetActive(true);
           Instantiate(bottleCrate);
           yield return new WaitForSeconds(crateSpawnRate);
       }
       
   }


   private void LifeLost() {
       misses++;
       SetMissText();

       if (misses >= 3) {
           gameOver = true;
           Debug.Log("Game Over");
       }
       Debug.Log("Lost Life");
   }

   private void GetPoints() {
       score++;
       SetScoreText();
       Debug.Log("Got A Point");
       
   }

   private void SetScoreText() {
       scoreText.text = score.ToString();
   }

   private void SetMissText() {
       if (misses <= 3) {
           missText.text = missTextList[misses];
       }
       
   }
   
   
   
   
   
}
