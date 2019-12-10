using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Causes ammo meter on HUD to pop when player
/// collects ammo for its corresponding weapon
/// </summary>
public class PopOnCollect : MonoBehaviour
{
    // meter pop configuration fields
    [SerializeField] WeaponType myWeapon;                   // weapon type represented by ammo meter
    [SerializeField] RectTransform popTransform;            // controls size of meter pop microinteraction
    [Range(0f, 10f)]
    [SerializeField] float popExpandRate = 1f;              // rate at which pop overlay expands to its peak scale
    [Range(0f, 5f)]
    [SerializeField] float popDiminishRate = 1f;            // rate at which pop diminishes after reaching peak scale

    // pop support variables
    CanvasGroup popCanvasGroup;                             // controls visibility of meter pop interaction
    Vector2 popPeakScale = new Vector2();                   // scale meter pop grows to
    IEnumerator popCoroutine;                               // coroutine controlling visibility and scale of pop image

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // if not set in editor, assume UI element popping is this object
        if (!popTransform)
            popTransform = GetComponent<RectTransform>();

        // retrieve relevant components/information
        popCanvasGroup = popTransform.GetComponent<CanvasGroup>();
        popPeakScale = popTransform.localScale;
    }

    /// <summary>
    /// Called before first frame of Update
    /// </summary>
    void Start()
    {
        // add self as listener to add weapon event
        EventManager.AddPickUpWeaponListener(HandleWeaponPickup);
    }

    /// <summary>
    /// Controls popping of ammo meter when user
    /// collects a weapon
    /// </summary>
    /// <param name="type">type of weapon collected</param>
    void HandleWeaponPickup(WeaponType type)
    {
        // if collected weapon matches object's weapon
        if (type == myWeapon)
        {
            // start/restart pop coroutine
            popCoroutine = PopMeter();
            StartCoroutine(popCoroutine);
        }
    }

    /// <summary>
    /// Causes ammo meter to 'pop' when player
    /// collects a weapon
    /// </summary>
    /// <returns></returns>
    IEnumerator PopMeter()
    {
        // initialize pop overlay
        popCanvasGroup.alpha = 1;

        // expand pop overlay to its peak
        float popProgress = 0f;
        do
        {
            popProgress += Time.unscaledDeltaTime * popExpandRate;
            popTransform.localScale = Vector2.Lerp(Vector2.zero, popPeakScale, popProgress);
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        } while ((Vector2)popTransform.localScale != popPeakScale);

        // shrink and fade overlay
        float diminishProgress = 0f;
        do
        {
            diminishProgress += Time.unscaledDeltaTime * popDiminishRate;
            popTransform.localScale = Vector2.Lerp(popPeakScale, Vector2.one, diminishProgress);
            popCanvasGroup.alpha = Mathf.Lerp(1, 0, diminishProgress);
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        } while (popCanvasGroup.alpha > 0);
    }

}
