using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Graphically displays player's remaining oxygen by scaling a meter
/// </summary>
public class O2Meter : MeterScaler
{
    // lazy fill support variables
    [SerializeField] RectTransform lazyFill;
    [Range(0f, 3f)]
    [SerializeField] float lazyFillWait = 1f;               // time 'lazy' oxygen meter fill waits before shrinking
    [Range(0f, 10f)]
    [SerializeField] float scaleRate = 1f;                  // rate at which lazy fill matches scale of meter
    [Range(1, 100)]
    [SerializeField] int significantLoss = 10;              // amount of oxygen player must lose to activate lazy fill
    IEnumerator lazyFillCoroutine;                          // coroutine controlling oxygen meter's lazy fill
    bool lazyFillRunning = false;

    /// <summary>
    /// Called before first frame of Update
    /// </summary>
    protected override void Start()
    {
        // add self as listener to Update O2 display event
        EventManager.AddUpdateO2Listener(UpdateDisplay);
    }

    /// <summary>
    /// Scales oxygen meter to passed in value,
    /// initializing meter's 'lazy' fill.
    /// </summary>
    /// <param name="newValue">new value from 0 - 100 to scale to</param>
    protected override void UpdateDisplay(float newValue)
    {
        // scale meter
        float loss = myRectTransform.localScale.x * 100 - newValue;
        base.UpdateDisplay(newValue);

        // if oxygen loss was significant
        if (loss >= significantLoss)
        {
            // start/restart coroutine to shrink lazy fill
            if (lazyFillRunning) StopCoroutine(lazyFillCoroutine);
            lazyFillCoroutine = LockLazyFill();
            StartCoroutine(lazyFillCoroutine);
        }
        // but if no lazy fill coroutine is running (damage isn't significant)
        else if (!lazyFillRunning)
            // gradually scale lazy fill with meter
            lazyFill.localScale = new Vector3(Mathf.Max(lazyFill.localScale.x - Mathf.Sign(loss) * scaleRate * Time.deltaTime, 
                myRectTransform.localScale.x), 1, 1);
    }

    /// <summary>
    /// Locks oxygen meter's lazy fill for duration
    /// to highlight damage dealt to player.
    /// </summary>
    /// <returns></returns>
    IEnumerator LockLazyFill()
    {
        // lock lazy fill's scale for wait duration
        lazyFillRunning = true;
        yield return new WaitForSeconds(lazyFillWait);
        lazyFillRunning = false;
    }
}
