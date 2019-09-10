using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInput : MonoBehaviour {

    public bool mario;
    public bool up;
    
    public delegate void ButtonPressed();

    public static event ButtonPressed MarioUp;
    public static event ButtonPressed MarioDown;
    public static event ButtonPressed LuigiUp;
    public static event ButtonPressed LuigiDown;

    
   
    
    #if (UNITY_ANDROID || UNITY_IOS)

            void Update()
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{

				Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);

				RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (MarioUp != null && hit.collider != null && hit.collider.tag == "MarioUp") {
                MarioUp();
            }else if (MarioDown != null && hit.collider != null && hit.collider.tag == "MarioDown") {
                MarioDown();
            }else if (LuigiUp != null && hit.collider != null && hit.collider.tag == "LuigiUp") {
                LuigiUp();
            }else if (LuigiDown != null && hit.collider != null && hit.collider.tag == "LuigiDown") {
                LuigiDown();
            }

				
			}
		}
	}
    
    #elif   UNITY_EDITOR
        private void Update() {
             if (Input.GetMouseButtonDown(0)) {
                 Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                 RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

             if (MarioUp != null && hit.collider != null && hit.collider.tag == "MarioUp") {
                 MarioUp();
             }else if (MarioDown != null && hit.collider != null && hit.collider.tag == "MarioDown") {
                 MarioDown();
             }else if (LuigiUp != null && hit.collider != null && hit.collider.tag == "LuigiUp") {
                  LuigiUp();
             }else if (LuigiDown != null && hit.collider != null && hit.collider.tag == "LuigiDown") {
                  LuigiDown();
             }
        }
    }
    
    
    
    
    
    #endif
//    private void OnMouseDown() {
//        if (MarioUp != null && mario && up) {
//            MarioUp();
//        }
//        
//        else if (MarioDown != null && mario) {
//            MarioDown();
//        }
//        
//        else if (LuigiUp != null && up) {
//            LuigiUp();
//        }
//        
//        else if (LuigiDown != null) {
//            LuigiDown();
//        }
//    }


}
