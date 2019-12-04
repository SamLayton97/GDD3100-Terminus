using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Detects user swipe gestures over pre-defined zone
/// </summary>
public class SwipeDetector : EventTrigger
{
    // swipe configuration variables
    [SerializeField]
    bool detectSwipeOnlyAfterRelease = true;            // determines whether to detect swipe during or after gesture release
    [Range(20f, 50f)]
    [SerializeField] float minSwipeDistance = 20f;      // min pixel distance finger must travel to read as swipe gesture

    // swipe support variables
    Vector2 fingerDownPosition;                         // finger screen position when user initiated swipe gesture
    //Vector2 fingerUpPosition;                           // finger screen position when user released swipe gesture

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
    /// Using pointer positions, determines whether to 
    /// register user input as a swipe gesture and in what
    /// direction
    /// </summary>
    /// <param name="deltaPosition">change in positions
    /// across start and end finger positions</param>
    void DetectSwipe(Vector2 deltaPosition)
    {
        // only check for significant swipes
        if (deltaPosition.magnitude >= minSwipeDistance)
        {
            // determine dominant axis and swipe direction
            bool verticalDominant = Mathf.Abs(deltaPosition.y) > Mathf.Abs(deltaPosition.x);
            
            // if swipe was veritcal
            if (verticalDominant)
            {
                // register swipe from direction on y-axis
                detectEvent.Invoke(deltaPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down);
            }
            // otherwise (horizontal swipe)
            else
            {
                // register swipe from direction on x-axis
                detectEvent.Invoke(deltaPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left);
            }
        }
    }

    #region Drag Event Handling Methods

    /// <summary>
    /// Adds given method as a listener to the Detect Swipe event
    /// </summary>
    /// <param name="newListener">new listening method</param>
    public void AddSwipeListener(UnityAction<SwipeDirection> newListener)
    {
        detectEvent.AddListener(newListener);
    }

    /// <summary>
    /// Called when pointer begins dragging across swipe zone
    /// </summary>
    /// <param name="eventData">data related to initial input</param>
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        // store starting finger position
        fingerDownPosition = eventData.position;
    }

    /// <summary>
    /// Called when pointer continues to drag across swipe zone
    /// </summary>
    /// <param name="eventData">continued input data</param>
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        // if swipes can register each frame
        if (!detectSwipeOnlyAfterRelease)
            // detect swipe motion from start to current
            DetectSwipe(eventData.position - fingerDownPosition);
    }

    /// <summary>
    /// Called when pointer stops dragging across swipe zone
    /// </summary>
    /// <param name="eventData">terminated data input</param>
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        // if swipes only register on release
        if (detectSwipeOnlyAfterRelease)
            // detect swipe motion from start and end
            DetectSwipe(eventData.position - fingerDownPosition);
    }

    #endregion

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

