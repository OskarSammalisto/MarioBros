using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   
   string sceneName = "SampleScene";
   private void OnMouseDown() {
      SceneManager.LoadScene(sceneName);
   }
}
