using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Reads pointer-based input from Weapons HUD, initiating
/// selection of appropriate weapon in player's possession.
/// Used for mobile controls.
/// </summary>
public class HUDReadSelectInput : MonoBehaviour
{
    // visual confirmation configuration
    [SerializeField] List<Image> flashImages =      // list of background images that flash to confirm input
        new List<Image>();                          // NOTE: must be entered in order their corresponding weapons appear in WeaponTypes enum
    [SerializeField] Color toColor;                 // color to flash to
    [SerializeField] float flashRate = 1f;          // rate at which visual confirmation flashes

    // visual confirmation support variables
    Color fromColor = Color.white;                  // color to flash from -- initial color of HUD element

    // event support
    HUDSelectWeaponEvent selectEvent;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // get initial color of confirmation to flash from
        if (flashImages.Count > 0)
            fromColor = flashImages[0].color;
    }

    /// <summary>
    /// Used for late initialization
    /// </summary>
    void Start()
    {
        // add self as invoker of HUD select event
        selectEvent = new HUDSelectWeaponEvent();
        EventManager.AddHUDSelectWeaponInvoker(this);
    }

    /// <summary>
    /// Initates weapon selection of particular type from HUD
    /// </summary>
    /// <param name="type">index of selected weapon type within
    /// WeaponTypes enum</param>
    public void SelectWeapon(int typeIndex)
    {
        // initiate weapon select and visual confirmation
        selectEvent.Invoke(typeIndex);
        StartCoroutine(ConfirmSelection(typeIndex));
    }

    /// <summary>
    /// Adds given method as listener to HUD select weapon event
    /// </summary>
    /// <param name="newListener">new method listening for event</param>
    public void AddSelectListener(UnityAction<int> newListener)
    {
        selectEvent.AddListener(newListener);
    }

    /// <summary>
    /// Confirms user's attepmted weapon selection
    /// </summary>
    /// <param name="flashIndex">index of background to flash</param>
    /// <returns></returns>
    IEnumerator ConfirmSelection(int flashIndex)
    {
        // retrieve background image to flash
        Image toFlash = flashImages[flashIndex];

        // flash between colors, switching direction when appropriate
        float flashProgress = 0f;
        bool ascending = true;
        do
        {
            // lerp between colors and reverse at peak
            flashProgress += flashRate * Time.unscaledDeltaTime * (ascending ? 1f : -1f);
            toFlash.color = Color.Lerp(fromColor, toColor, flashProgress);
            if (flashProgress >= 1)
                ascending = !ascending;

            // wait for next frame
            yield return new WaitForEndOfFrame();

        } while (flashProgress > 0);
    }

}
