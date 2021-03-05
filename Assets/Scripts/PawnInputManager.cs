using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnInputManager : MonoBehaviour
{
    private float start;
    private float delta;

    private PawnController pawn;

    private void Awake()
    {
        pawn = GetComponent<PawnController>();
    }
    void FixedUpdate()
    {
        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    start = touch.position.y;
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    delta = touch.position.y - start;
                    start = touch.position.y;
                    pawn.ModifyPercentage(-delta / 500);
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    break;
            }
        }
    }
}
