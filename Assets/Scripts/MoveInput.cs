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

    private void OnMouseDown() {
        if (MarioUp != null && mario && up) {
            MarioUp();
        }
        
        else if (MarioDown != null && mario) {
            MarioDown();
        }
        
        else if (LuigiUp != null && up) {
            LuigiUp();
        }
        
        else if (LuigiDown != null) {
            LuigiDown();
        }
    }
}
