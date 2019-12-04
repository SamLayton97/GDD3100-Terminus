using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects user swipe gestures over pre-defined zone
/// Based on source: https://www.youtube.com/watch?v=jbFYYbu5bdc
/// </summary>
public class SwipeDetector : MonoBehaviour
{
    // swipe configuration variables
    [SerializeField]
    bool detectSwipeOnlyAfterRelease = true;    // determines whether to detect swipe during or after gesture release

    // swipe support variables
    Vector2 fingerDownPosition;                 // finger screen position when user initiated swipe gesture
    Vector2 fingerUpPosition;                   // finger screen position when user released swipe gesture


}

/// <summary>
/// Enumeration of all possible swipe directions
/// read by detector
/// </summary>
public enum SwipeDirections
{
    Up,
    Down,
    Left,
    Right
}

