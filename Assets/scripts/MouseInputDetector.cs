using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputDetector : MonoBehaviour
{
    private bool isPressed = false;
    private Vector2? pressPosition;

    public bool IsPressed
    {
        get { return isPressed; }
    }

    public Vector2? PressPosition
    {
        get { return pressPosition; }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            if(pressPosition == null) {
                pressPosition = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
            pressPosition = null;
        }
    }
}
