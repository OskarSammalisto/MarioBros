using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
 
 [SerializeField]
 private List<Sprite> doorSprites = new List<Sprite>();

 private SpriteRenderer spriteRenderer;

 private void Start() {
  spriteRenderer = GetComponent<SpriteRenderer>();
  CloseDoor();
 }

 public void OpenDoor() {
     spriteRenderer.sprite = doorSprites[1];
 }

 public void CloseDoor() {
     spriteRenderer.sprite = doorSprites[0];
 }
    
}
