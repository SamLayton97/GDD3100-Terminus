using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Graphically displays player's remaining oxygen by scaling a meter
/// </summary>
public class O2Meter : MeterScaler
{
    // lazy fill support variables
    [Range(0f, 3f)]
    [SerializeField] float lazyFillWait = 1f;               // time 'lazy' oxygen meter fill waits before shrinking
    [Range(0f, 2f)]
    [SerializeField] float scaleRate = 1f;                  // rate at which lazy fill matches scale of meter
    [Range(-100, 1)]
    [SerializeField] int significantLoss = -10;             // amount of oxygen player must lose to activate lazy fill

    // lazy loss support variables
    [SerializeField] RectTransform backFill;                // fill appearing behind meter -- used for 'lazy' O2 loss
    IEnumerator lazyLossCoroutine;                          // coroutine controlling hurt meter's lazy fill
    bool lazyLossRunning = false;

    // lazy regain support variables
    [SerializeField] RectTransform foreFill;                // fill appearing in front of meter -- used for 'lazy' O2 regain
    IEnumerator lazyRegainCoroutine;                        // coroutine controlling regain meter's lazy fill
    bool lazyRegainRunning = false;

    // meter flash support variables
    [SerializeField] CanvasGroup meterFlash;                // controls visibility of flash overlay
    [Range(0f, 10f)]
    [SerializeField] float flashRate = 1f;                  // rate at which meter overlay flashes
    IEnumerator flashCoroutine;                             // coroutine controlling visibility of flash image

    // meter pop support variables
    [SerializeField] RectTransform popTransform;            // controls size of meter pop microinteraction
    CanvasGroup popCanvasGroup;                             // controls visibility of meter pop interaction
    Vector2 popTargetScale = new Vector2();                 // scale meter pop grows to

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
        // find change in oxygen value and scale meter
        float oxygenChange = newValue - fillTransform.localScale.x * 100;
        base.UpdateDisplay(newValue);

        // if player lost significant amount of oxygen
        if (oxygenChange <= significantLoss)
        {
            // start/restart coroutine to lock lazy loss
            if (lazyLossRunning) StopCoroutine(lazyLossCoroutine);
            lazyLossCoroutine = LockLazyLoss();
            StartCoroutine(lazyLossCoroutine);

            // start coroutine to cause meter to flash
            flashCoroutine = FlashMeter();
            StartCoroutine(flashCoroutine);
        }
        // but if player regained oxygen
        else if (oxygenChange > 0)
        {
            // start/restart coroutine to lock lazy regain
            if (lazyRegainRunning) StopCoroutine(lazyRegainCoroutine);
            lazyRegainCoroutine = LockLazyRegain();
            StartCoroutine(lazyRegainCoroutine);
        }

        // gradually scale unlocked lazy meters
        backFill.localScale = new Vector3(Mathf.Max(backFill.localScale.x +
                Mathf.Sign(oxygenChange) * scaleRate * Time.deltaTime * (lazyLossRunning ? 0 : 1),
                fillTransform.localScale.x), 1, 1);
        foreFill.localScale = new Vector3(Mathf.Min(foreFill.localScale.x -
                Mathf.Sign(oxygenChange) * scaleRate * Time.deltaTime * (lazyRegainRunning ? 0 : 1),
                fillTransform.localScale.x),
                1, 1);
    }

    /// <summary>
    /// Locks oxygen meter's lazy loss fill for duration
    /// to highlight damage dealt to player.
    /// </summary>
    /// <returns></returns>
    IEnumerator LockLazyLoss()
    {
        // lock lazy fill's scale for wait duration
        lazyLossRunning = true;
        yield return new WaitForSeconds(lazyFillWait);
        lazyLossRunning = false;
    }

    /// <summary>
    /// Locks oxygen meter's lazy regain fill for duration
    /// to highlight oxygen player received.
    /// </summary>
    /// <returns></returns>
    IEnumerator LockLazyRegain()
    {
        // lock lazy fill's scale for wait duration
        lazyRegainRunning = true;
        yield return new WaitForSeconds(lazyFillWait);
        lazyRegainRunning = false;
    }

    /// <summary>
    /// Causes oxygen meter to flash when player loses significant 
    /// amount by controlling opacity of overlay image.
    /// </summary>
    /// <returns></returns>
    IEnumerator FlashMeter()
    {
        bool ascending = true;
        do
        {
            // increase/decrease opacity of flash overlay, reversing direction when appropriate
            meterFlash.alpha += flashRate * Time.deltaTime * (ascending ? 1 : -1);
            if (meterFlash.alpha >= 1) ascending = !ascending;
            yield return new WaitForEndOfFrame();

        } while (meterFlash.alpha > 0);
    }

    /// <summary>
    /// Causes oxygen meter to 'pop' when player gains oxygen
    /// </summary>
    /// <returns></returns>
    IEnumerator PopMeter()
    {
        yield return new WaitForEndOfFrame();
    }
}
