using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Detects user swipe gestures over pre-defined zone
/// Based on source: https://www.youtube.com/watch?v=jbFYYbu5bdc
/// </summary>
public class SwipeDetector : MonoBehaviour
{
    // swipe configuration variables
    [SerializeField]
    bool detectSwipeOnlyAfterRelease = true;            // determines whether to detect swipe during or after gesture release
    [Range(20f, 50f)]
    [SerializeField] float minSwipeDistance = 20f;      // min pixel distance finger must travel to read as swipe gesture

    // swipe support variables
    Vector2 fingerDownPosition;                         // finger screen position when user initiated swipe gesture
    Vector2 fingerUpPosition;                           // finger screen position when user released swipe gesture

    // event support
    DetectSwipeEvent detectEvent;

    /// <summary>
    /// Used for late initialization
    /// </summary>
    void Start()
    {
        // add self as invoker of detect swipe event
        detectEvent = new DetectSwipeEvent();
        EventManager.AddDetectSwipeInvoker(this);
    }

    /// <summary>
    /// Adds given method as a listener to the Detect Swipe event
    /// </summary>
    /// <param name="newListener">new listening method</param>
    public void AddSwipeListener(UnityAction<SwipeDirection> newListener)
    {
        detectEvent.AddListener(newListener);
    }

    /// <summary>
    /// Called when user's finger presses down on swipe zone.
    /// </summary>
    public void InitiateSwipe()
    {
        Debug.Log("start swipe");
    }

    /// <summary>
    /// Called each frame user's finger continues to press
    /// on swipe zone.
    /// </summary>
    public void UpdateSwipe()
    {
        Debug.Log("update swipe");
    }

    /// <summary>
    /// Called when user releases finger from swipe zone.
    /// </summary>
    public void ReleaseSwipe()
    {
        Debug.Log("release swipe");
    }

}

/// <summary>
/// Enumeration of all possible swipe directions
/// read by detector
/// </summary>
public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right
}

