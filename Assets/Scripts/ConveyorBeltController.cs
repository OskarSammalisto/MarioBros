using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltController : MonoBehaviour {
    private float changeTime;
    private float changeInterval = 0.5f;
    private SpriteRenderer spRenderer;

    private void Start() {
        spRenderer = GetComponent<SpriteRenderer>();
        changeTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= changeTime + changeInterval) {
            spRenderer.flipX = !spRenderer.flipX;
            changeTime = Time.time;
        }
    }
}
