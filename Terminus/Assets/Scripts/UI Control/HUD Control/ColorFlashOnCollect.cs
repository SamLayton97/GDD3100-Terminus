﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Causes appropriate HUD element to flash between two
/// colors when player collects a new weapon.
/// </summary>
[RequireComponent(typeof(Image))]
public class ColorFlashOnCollect : MonoBehaviour
{
    // configuration variables
    [SerializeField] Color toColor;             // color to flash to
    [SerializeField] float flashTime = 1f;      // time (seconds) to flash from one color to another

    // support variables
    Image flashImage;
    Color fromColor;
    IEnumerator flashCoroutine;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components/information
        flashImage = GetComponent<Image>();
        fromColor = flashImage.color;
    }

    /// <summary>
    /// Called before first frame Update()
    /// </summary>
    void Start()
    {
        // add self as listener to add weapon event
        EventManager.AddPickUpWeaponListener(HandleWeaponPickup);
    }

    /// <summary>
    /// Starts coroutine to flash HUD element when
    /// player pickups up weapon.
    /// </summary>
    /// <param name="type">Weapon type collected.
    /// NOTE: discarded; only needed to listen for Pick Up Weapon event.</param>
    void HandleWeaponPickup(WeaponType type)
    {
        if (flashCoroutine != null) StopCoroutine(flashCoroutine);
        flashCoroutine = FlashHUDElement(flashTime);
        StartCoroutine(flashCoroutine);
    }

    /// <summary>
    /// Shifts color of HUD element from one color to another and back
    /// over given period of time.
    /// </summary>
    /// <param name="flashTime"></param>
    /// <returns></returns>
    IEnumerator FlashHUDElement(float flashTime)
    {
        // declare support variables
        bool movingTo = true;
        float lerpProgress = 0f;

        // while image color hasn't returned to start
        do
        {
            // shift color by increment, reversing direction at apex
            lerpProgress += Time.deltaTime * (2f / flashTime) * (movingTo ? 1 : -1);
            flashImage.color = Color.Lerp(fromColor, toColor, lerpProgress);
            if (flashImage.color == toColor)
                movingTo = !movingTo;

            // wait a frame before lerping again
            yield return new WaitForEndOfFrame();

        } while (flashImage.color != fromColor);
    }
}